

        //MODULO PRONOSTICO ANGULARJS
        var pronosticoExtendido = angular.module('pronosticoExtendido', ['ngStorage']);


        //INTERCEPTOR DE PETICIONES
        pronosticoExtendido.factory('httpInterceptor', function ($q, $rootScope, $location, $localStorage) {

            return {
                'request': function (config) {
                    //Si existe el token entonces el usuario se ecnuentra autenticado
                    if ($localStorage.token) 
                    {
                        $rootScope.authenticated = true;
                        config.headers = angular.extend({}, config.headers, { "Authorization": "Bearer " + $localStorage.token });
                    }
                    else
                    {
                        $rootScope.authenticated = false;
                    }
                    return config;
                },
                'responseError': function (response) {
                    //Caaptura de errores de servicio
                    if (response.status === 401 || response.status === 403)
                    {
                        $localStorage.token = null;
                        $rootScope.authenticated = false;
                        $rootScope.modalMessage = "Sesion caducada, vuelva a identificarse";
                        $rootScope.modalTitle = "No autorizado para acceder";
                        $('#modal').modal();
                    }
                    alert(response.status);
                    if (response.status === 409) {
                        $localStorage.token = null;
                        $rootScope.authenticated = false;
                        $rootScope.modalMessage = "Ocurrio un error al consumir el servicio, intente mas tarde";
                        $rootScope.modalTitle = "Error de Servidor";
                        $('#modal').modal();
                    }

                    return $q.reject(response);
                    
                }
            }
        });

        pronosticoExtendido.config(function ($httpProvider)
        {
            $httpProvider.interceptors.push('httpInterceptor');
            $httpProvider.defaults.withCredentials = true;
        });
     
        //CONTROLADOR GENERAL
        pronosticoExtendido.controller('pronostico', function ($scope, $http, $localStorage, $rootScope)
        {
            /**************** FUNCION LOGIN *******************/
            $scope.Login = function (user)
            {
                if ($localStorage.token == null)
                {
                    $http({
                        method: 'POST',
                        url: $("#serviceURL").val() + 'api/login/authenticate',
                        data: { Username: user.name, Password: user.password },
                        headers: {
                            'Content-Type': 'application/json'
                        }
                    }).then(function successCallback(response) {
                        $localStorage.token = response.data;
                        $rootScope.authenticated = true;
                        $rootScope.modalMessage = "Logueado con exito";
                        $rootScope.modalTitle = "Usuario Valido";
                        $('#modal').modal();
                        $scope.Inicializacion();
                    },
                       function errorCallback(response) {
                           $rootScope.modalMessage = "Usuario o Password incorrecto";
                           $rootScope.modalTitle = "Login incorrecto";
                           $('#modal').modal();
                       });
                }
                else
                {
                    $rootScope.modalMessage = "Ya se encuentra logueado";
                    $rootScope.modalTitle = "Error";
                    $('#modal').modal();
                }
           
            }

            /**************** FUNCION OBTENER PRONOSTICO *******************/
            $scope.CurrentWeather = function ()
            {
                $scope.weekday = [];
                $scope.weekday[0] = "Domingo";
                $scope.weekday[1] = "Lunes";
                $scope.weekday[2] = "Martes";
                $scope.weekday[3] = "Miercoles";
                $scope.weekday[4] = "Jueves";
                $scope.weekday[5] = "Viernes";
                $scope.weekday[6] = "Sabado";


                //Llamada a servicio (por latitud y longitud para asegurar presicion y fallos de busqueda)
                $http({
                    method: 'POST',
                    url: $("#serviceURL").val() + 'api/weather/getweather',
                    data: { Country: $scope.selectedCountry.name, State: $scope.selectedState.name },
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }).then(function successCallback(response) {
                    $scope.weather = response.data.current;

                    //Obtengo la fecha del clima (timestamp)
                    var today = new Date(response.data.current.dt * 1000);
                    $scope.curDate = $scope.weekday[today.getDay()];

                    //Obtengo el icono correspondiente al json de iconos
                    var code = $scope.weather.weather[0].id;
                    var icon = $scope.icons[code].icon;
                    if (!(code > 699 && code < 800) && !(code > 899 && code < 1000)) {
                        icon = 'day-' + icon;
                    }
                    $scope.curIconClass = 'wi wi-' + icon;

                    //Muestro nombre de Pais y ciudad
                    $scope.curSelectedCountry = $scope.selectedCountry.name;
                    $scope.curSelectedState = $scope.selectedState.name;



                    //Forecast
                    angular.forEach(response.data.daily, function (d, key) {
                        //Obtengo dia de la semana
                        var today = new Date(d.dt * 1000);
                        d.day = $scope.weekday[today.getDay()];

                        //Obtengo icono
                        var code = d.weather[0].id;
                        var icon = $scope.icons[code].icon;

                        if (!(code > 699 && code < 800) && !(code > 899 && code < 1000)) {
                            icon = 'day-' + icon;
                        }

                        d.icon = 'wi wi-' + icon;
                    }, []);

                    $scope.forecasts = response.data.daily.splice(1,6);

                    //Muestro los datos finalmente
                    $scope.showCurrentWeather = true;
                    $scope.showForecasts = true;
                });
            };

            /**************** FUNCION OBTENER CLIMA GENERAL *******************/
            $scope.searchWeather = function () {
                $scope.showCurrentWeather = false;
                $scope.showForecasts = false;
                $scope.CurrentWeather();
            };

            /**************** FUNCION OBTENER CIUDADES *******************/
            $scope.searchState = function () {
                $http({
                    method: 'POST',
                    url: $("#serviceURL").val() + 'api/region/getcities',
                    data: { StateId: $scope.selectedCountry.id },
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }).then(function successCallback(response) {
                    $scope.cities = response.data;
                });
            };

            /**************** FUNCION INICIALIZACION *******************/
            $scope.Inicializacion = function ()
            {
                //Oculto paneles
                $scope.showForecasts = false;
                $scope.showCurrentWeather = false;

                //Obtencion codigo de iconos de clima
                $http({
                    method: 'GET',
                    url: '/assets/icons/icons.json'
                }).then(function successCallback(response) {
                    $scope.icons = response.data;

                });
         
                //Cargo dropdownlist paises
                $http({
                    method: 'POST',
                    url: $("#serviceURL").val() + 'api/region/getcountries',
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }).then(function successCallback(response)
                {

                    $scope.paises = response.data;
                    $scope.selectedCountry = $scope.paises[10];

                    //Cargo dropdownlist ciudades
                    $http({
                        method: 'POST',
                        url: $("#serviceURL").val() + 'api/region/getcities',
                        data: { StateId: $scope.selectedCountry.id },
                        headers: {
                            'Content-Type': 'application/json'
                        }
                    }).then(function successCallback(response) {
                        $scope.cities = response.data;
                        $scope.selectedState = $scope.cities[0];;
                        $scope.country = $('#country').children("option:selected").attr("label");
                        $scope.state = $('#state').children("option:selected").attr("label");

                        $scope.CurrentWeather();

                        $scope.test = true;

                    });
                });;
                

            }
            
            /**************** CHECKEAR AUTENTICACION PAGE LOAD *******************/
            if ($localStorage.token)
            {
                $rootScope.authenticated = true;
                $scope.Inicializacion();
            }

        });

