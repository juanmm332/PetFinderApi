# PetFinderApi

## MySQL

La API usa MySQL mediante Entity Framework Core y el proveedor Pomelo. Antes de
iniciarla, reemplaza `CHANGE_ME` en `PresentationLayer/appsettings.json` por la
contraseña local de MySQL, o configura la variable de entorno
`ConnectionStrings__DefaultConnection` con una cadena de conexión válida.

Ejemplo de cadena de conexión:

```text
Server=localhost;Port=3306;Database=PetFinder;User=root;Password=tu_contraseña;
```

Al arrancar, la API aplica las migraciones pendientes. Para aplicarlas
manualmente desde la raíz del repositorio:

```powershell
dotnet ef database update --project .\DataAccessLayer\DataAccessLayer.csproj --startup-project .\PresentationLayer\PresentationLayer.csproj
```

La API permite solicitudes CORS desde `http://localhost:3000` y
`https://localhost:3000` para el frontend local.

Los animales requieren `Nombre`, `Foto` (URL o ruta), `IdUsuario` y aceptan un
`Comentario` opcional. `Id` lo genera la base de datos; cada animal pertenece a
un usuario y se elimina en cascada si se elimina ese usuario.

Los avisos usan el enum `AvisoEstado` (`Activo`, `Encontrado`, `Cerrado`), se
relacionan con un animal mediante `IdAnimal` y contienen `UltimaUbicacionVista`
y un comentario opcional. El estado se almacena como entero en MySQL y la API lo
representa con el nombre del enum.

Los comentarios tienen `Texto`, `Fecha`, `IdUsuario` e `IdAviso`; las claves
foráneas los vinculan con el usuario autor y el aviso. Al eliminar un usuario o
un aviso, sus comentarios relacionados se eliminan en cascada.

Los comentarios contienen `Texto`, `IdUsuario`, `IdAviso` y `Fecha`, con claves
foráneas hacia el usuario y el aviso. Al eliminar cualquiera de esos registros,
sus comentarios relacionados se eliminan en cascada.

## Eliminación lógica y permisos

`Animal`, `Aviso`, `Comentario` y `Usuario` tienen la columna `Eliminado` para
indicar si el registro está eliminado. `Usuario` también tiene la columna
`EsAdministrador` para indicar si cuenta con permisos de administrador. Las
columnas nuevas se agregan mediante una migración y empiezan en `false` para
los registros existentes; las migraciones pendientes se aplican al iniciar la
API.

## Operaciones CRUD

Los controladores de `Usuario`, `Animal`, `Aviso` y `Comentario` ofrecen GET de
lista y por ID, POST, PUT y DELETE. DELETE realiza eliminación lógica al marcar
`Eliminado=true`; los elementos eliminados no aparecen en los GET y no se
pueden actualizar mediante PUT. Las relaciones y fechas de creación no se
pueden cambiar mediante los PUT. El usuario se identifica mediante el claim del
token JWT, no mediante un ID declarado por el cliente. Un usuario puede
actualizar y borrar sus propios registros; el administrador puede operar sobre
cualquier registro. Solo un administrador puede restaurar con
`PATCH /api/{recurso}/{id}/restore`.

El registro `POST /api/Usuario` y `POST /api/Auth/login` son públicos. El resto
de rutas CRUD requiere `Authorization: Bearer <token>`. Los usuarios nuevos no
son administradores. Para asignar el primer administrador, registre una cuenta,
inicie sesión después de asignar el rol en la base de datos y vuelva a iniciar
sesión para obtener un token con el claim de administrador:

```sql
UPDATE Usuarios SET EsAdministrador = TRUE WHERE Correo = 'admin@example.com';
```

El correo debe ser único entre todas las cuentas, incluidas las eliminadas, y
las contraseñas nuevas deben tener al menos 5 caracteres.

La API requiere un secreto JWT aleatorio de al menos 32 bytes. Para desarrollo,
guárdelo con User Secrets (el proyecto ya tiene configurado `UserSecretsId`):

```powershell
$rng = [Security.Cryptography.RandomNumberGenerator]::Create()
$bytes = New-Object byte[] 32
$rng.GetBytes($bytes)
$key = [Convert]::ToBase64String($bytes)
dotnet user-secrets set "Jwt:SigningKey" $key --project .\PresentationLayer\PresentationLayer.csproj
$rng.Dispose()
```

En producción, configure `Jwt__SigningKey` mediante el almacén de secretos del
entorno. No guarde la clave en el repositorio.

El token expira en una hora. En VS Code, ejecute primero el registro y login en
`PresentationLayer.http`; las solicitudes posteriores reutilizan el token de
la respuesta del login. Para las rutas de restauración, reemplace
`@adminToken` por un token emitido a un administrador.

Las contraseñas nuevas se guardan usando `PasswordHasher`. Las cuentas que
todavía tengan una contraseña heredada en texto plano pueden iniciar sesión con
ella una vez; al iniciar sesión correctamente, la API la reemplaza por un hash.
Proteja la API con HTTPS y use un secreto diferente por entorno.