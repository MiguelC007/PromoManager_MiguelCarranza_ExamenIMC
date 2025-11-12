Buenas nches, Miguel Carranza Avilez - 62211533


esta Estructura contiene lo siguiente del Examens
El proyecto está dividido en capas principales, siguiendo una arquitectura limpia:

PromoManager.Domain
Contiene la lógica de negocio pura:

Entities: define las entidades principales, como Coupon.

ValueObjects: objetos de valor que encapsulan atributos importantes.

Interfaces: contratos para lectura y escritura de cupones (ICouponReader, ICouponWriter).

Exceptions: manejo de errores de dominio (DomainException).

PromoManager.Application
Contiene la lógica de aplicación:

Services: CouponService implementa la funcionalidad principal.

DTOs: objetos que transfieren información entre capas.

Mappers: convierten entre entidades y DTOs (CouponMapper).

Requests: definiciones de solicitudes como CreateCouponRequest.

PromoManager.Infrastructure
Contiene la persistencia y la implementación de repositorios:

Repositories: InMemoryCouponRepository para almacenar temporalmente los cupones.

UnitOfWork: controla la consistencia de las operaciones.
