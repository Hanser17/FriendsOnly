# 🏦 NetBanking

**NetBanking** es una aplicación web orientada a la gestión bancaria desarrollada con ASP.NET Core y Razor Pages.
Está construida con una arquitectura robusta, modular y escalable basada en **ONION Architecture**.

---

## 🧱 Stack Tecnológico

### 🌐 Web App con Razor Pages

- 🧅 **Arquitectura ONION**
- 📦 **Repositorio Genérico**
- 🔄 **Servicio Genérico**
- 🔐 **Identity** para autenticación y autorización
- 📧 **MailKit** para envío de correos electrónicos
- 🗺 **AutoMapper** para el mapeo entre:
  - ViewModels
  - Entidades (Entities)
  - DTOs

---

## 📁 Estructura del Proyecto

/NetBanking
│
├── Application # Lógica de negocio, servicios, validaciones
├── Domain # Modelos del dominio y contratos
├── Infrastructure # Implementación de persistencia y servicios externos
├── WebApp # Proyecto principal Razor (UI y controladores)
└── Shared # Recursos compartidos, utilidades, constantes
