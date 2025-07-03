# 📦 SmartInvoice

**SmartInvoice** es un sistema web basado en .NET Core para automatizar y controlar el flujo completo de facturación electrónica en una empresa. El sistema permite la carga automática de facturas desde correo electrónico (PDF y XML), validación y aprobación por roles, y evidencia del pago final.

---

## 🚀 Características Principales

- 📥 Captura de facturas desde correos electrónicos
- 📄 Lectura automática de archivos XML para extraer datos clave
- 🗃 CRUD de facturas vía API (REST)
- 🔐 Autenticación de usuarios con JWT
- 👥 Roles de usuario: `Admin`, `User`, etc.
- ✅ Flujo de aprobación de facturas paso a paso
- 💳 Registro de pago con evidencia documental
- 🔎 Swagger UI para probar y documentar la API
- 🛡 Protección de endpoints con `[Authorize]` y control de roles
- 📊 Pronto: panel administrativo y reportes

---

## 🧱 Tecnologías Utilizadas

| Componente              | Tecnología                     |
|-------------------------|--------------------------------|
| Backend API             | ASP.NET Core Web API (.NET 8+) |
| Base de Datos           | SQL Server                     |
| ORM / DB Access         | Entity Framework Core          |
| Autenticación           | ASP.NET Core Identity + JWT    |
| Lector XML              | LINQ to XML                    |
| Lector de correo        | (Próximo: MailKit/IMAP)        |
| Documentación API       | Swagger (Swashbuckle)          |

---

## 📂 Estructura del Proyecto

SmartInvoice/
│
├── SmartInvoice.API → Proyecto Web API (entry point)
├── SmartInvoice.Application → Lógica de aplicación y servicios
├── SmartInvoice.Domain → Entidades puras del dominio
├── SmartInvoice.Infrastructure → Persistencia, seguridad, correo, etc.
├── SmartInvoice.Tests → Pruebas unitarias
└── README.md → Este archivo


---

## 🧪 Endpoints Principales

### 🔑 Autenticación

- `POST /api/auth/register` → Registro de usuario
- `POST /api/auth/login` → Generación de JWT

### 🧾 Facturas

- `GET /api/facturas` → Listar
- `POST /api/facturas` → Crear
- `PUT /api/facturas/{id}` → Actualizar
- `DELETE /api/facturas/{id}` → Eliminar

### 👤 Usuarios (solo Admin)

- `GET /api/users` → Listar usuarios
- `GET /api/users/{id}/roles` → Roles del usuario
- `POST /api/users/{id}/roles/{rol}` → Asignar rol
- `DELETE /api/users/{id}/roles/{rol}` → Quitar rol

---

## 🛠 Configuración Inicial

1. Clona el repositorio:
```bash
git clone https://github.com/estebanberrio/SmartInvoice.git

2. Crea base de datos desde las migraciones:
```bash
dotnet run --project SmartInvoice.API

3. Ejecuta la app:
```bash
dotnet run --project SmartInvoice.API

4. Abre Swagger en:
```bash
https://localhost:xxxx/swagger

---

👤 Usuario Admin por defecto

| Email                                                       | Contraseña |
| ----------------------------------------------------------- | ---------- |
| [admin@smartinvoice.local                                   | Admin123!  |

---

📌 Próximas funcionalidades

📧 Captura automática de correos con facturas

📥 Upload y validación de soporte de pago

📈 Dashboard de facturación mensual

📄 Exportación a Excel/PDF

📤 Notificaciones por correo


---

🤝 Contribuciones
¿Deseas aportar? ¡Bienvenido!

Usa Issues para reportar errores o sugerencias

Haz un Fork y envía tu PR

---

📃 Licencia
MIT © 2025 - Desarrollado por Juan Esteban Berrío.
