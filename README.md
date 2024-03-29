
# America Virtual - Pronostico Extendido
Aplicacion para la consulta del pronostico extendido del tiempo a 5 dias, por estado y provincia.

# Arquitectura de la solucion
La solución esta desarrollada mediante dos Proyectos distintos. Separados en dos soluciones distintas.

**Proyecto 1: Servicio WebAPI compuesto por 3 controladores:**
	

 - **LoginController:** Encargado de la autenticacion mediante el mecanismo JWT Token 	
 - **RegionController:** Encargado de consumir los archivos json
   que contienen países y provincias
 - **WeatherController:** Encargado de consumir el servicio de clima

**Proyecto 2: Proyecto de MVC con una capa de frontend desarrollada en AngularJS (Version 1.3)** 

> Inicialmente pensé en desarrollar todo el proyecto en MVC sin la
> necesidad de AngularJS, pero debido a el enunciado me parecio ideal utilizar localStorage, por lo que pensé en un Microservicio consumido con AngularJS.


# Pasos para utilizarlo

A continuación una breve descripción del funcionamiento general del aplicativo.
## 1 - Inicio de Sesion
Ingrese su usuario demo, formado por el nombre **user** y contraseña **password**.
![Login](https://i.imgur.com/8wWEs8a.png)

## 2 - Carga del Clima

El aplicativo por defecto cargara el clima correspondiente a la zona de Argentina, Buenos Aires. 
![Carga del clima](https://i.imgur.com/InvaFHG.png)

## 3-Selección de Clima Deseado

Mediante las listas desplegables, usted puede seleccionar un país, y a continuación automáticamente el sistema le cargara en la lista inferior las provincias. Al presionar en BUSCAR se cargara el clima de la zona elegida.
![Carga de clima](https://i.imgur.com/MrzsPFC.png)
## 4-Sesión Caducada

La sesión por defecto en el sistema tiene duración a modo muestra de 1 minuto. Al expirar, la misma muestra el siguiente mensaje. Para volver a comenzar inicie sesión nuevamente.
![Sesion Caducada](https://i.imgur.com/HJ1LCQB.png)



# Despliegue del Aplicativo

**Servicio WebAPI:**

El Servicio Web posee un archivo de configuración (**Web.config**) en el cual pueden modificarse los siguientes parámetros:

> Observar que el tiempo de expiracion del token se establecio en 1
> minuto para corroborar el cierre de sesión en la WebAPI, en el caso de que se desee mayor duración, cambiar.
> Ademas tener en cuenta que en el caso del vencimiento de las Key de cada servicio, como el de Geolocalizacion y Clima pueden cambiarse o alterarse desde este archivo.

	

    <!--PARAMETROS DE JWT TOKEN-->
    <add key="JWT_SECRET_KEY" value="america-virtual-test-29042020" />
    <add key="JWT_AUDIENCE_TOKEN" value="http://localhost:49220" />
    <add key="JWT_ISSUER_TOKEN" value="http://localhost:49220" />
    <add key="JWT_EXPIRE_MINUTES" value="1" />   

    <!--URL DE LA APLICACION MVC POR DONDE LLEGAN LOS REQUEST-->
    <add key="CORS_URL" value="http://localhost:13182" />
    
    <!--KEYS DE SERVICIOS-->
    <add key="GEO_SERVICE_KEY" value="def23292fc6b4dc4a3bcaa4bfe72bb7f" />
    <add key="WEATHER_SERVICE_KEY" value="f6e3317899d314098847653f5fd29126" /> 

   
   **Aplicacion MVC:**

    <!--URL DEL SERVICIO A CONSUMIR-->
    <add key="serviceURL" value="http://localhost:49220/" />
     



