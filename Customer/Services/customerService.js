customerApp.service('CustomerService', function ($http, $q) {

    function searchCustomer(searchKey) {
        var q = $q.defer();
        $http({
            url: 'http://localhost:50383/api/customer/searchcustomer',
            method: 'GET',
            params: { searchKey: searchKey }
        }).success(function (response) {
            q.resolve(response);
        });

        return q.promise;
    }

    function getCustomer(customerId) {
        var q = $q.defer();
        $http({
            url: 'http://localhost:50383/api/customer/getcustomer',
            method: 'GET',
            params: { customerId: customerId }
        }).success(function (response) {
            q.resolve(response);
        });

        return q.promise;
    }

    function saveCustomer(customer) {
        var q = $q.defer();
        $http({
            url: 'http://localhost:50383/api/customer/savecustomer',
            method: 'POST',
            data: customer
        }).success(function (response) {
            q.resolve(response);
        });

        return q.promise;
    }

    function deleteCustomer(customerId) {
        var q = $q.defer();
        $http({
            url: 'http://localhost:50383/api/customer/deletecustomer',
            method: 'GET',
            params: { customerIdToDelete: customerId }
        }).success(function (response) {
            q.resolve(response);
        });

        return q.promise;
    }

    return {
        getCustomer: getCustomer,
        searchCustomer: searchCustomer,
        saveCustomer: saveCustomer,
        deleteCustomer: deleteCustomer
    };
});

