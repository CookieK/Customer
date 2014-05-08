customerApp.controller('CustomerController', function (CustomerService, $scope) {
    $scope.customers = [];

    $scope.searchResult = [];
    $scope.searchValue = '';
    $scope.ok = false;

    $scope.returnTotalSearchResults = function () {
        return $scope.searchResult.length;
    };

    $scope.searchByName = function () {
        $scope.searchResult = [];
        if ($scope.searchValue.length > 1) {
           CustomerService.searchCustomer($scope.searchValue)
            .then(function (customers)
            {
                _.forEach(customers.CustomerList, function(result) {
                    var searchResultExists = _.find($scope.searchResult, function(value) { return value.Id == result.Id; });
                    if (!searchResultExists) {
                        $scope.searchResult.push({ Name: result.Name, Id: result.Id });
                    }
                });
            });
        }
    };

    $scope.addNewCustomer = function () {
        $scope.ok = false;
        var customer = { Name: $scope.newCustomerName };
        CustomerService.saveCustomer(customer)
        .then(function (cust) {
            $scope.customer = cust;
            $scope.customers.push({ Name: $scope.customer.Name, Id: $scope.customer.Id });
            $scope.ok = true;

            $scope.searchByName();
        });

        $scope.newCustomerName = '';
    };

    $scope.editThisCustomer = function (customerId) {
        $scope.ok = false;
        CustomerService.getCustomer(customerId)
          .then(function (cust) {
              $scope.customer = cust;
        });
    };

    $scope.editCustomer = function () {
        $scope.ok = false;
        CustomerService.saveCustomer($scope.customer)
        .then(function (cust) {
            $scope.customer = cust;
            $scope.ok = true;

            $scope.searchByName();
        });
    };

    $scope.deleteThisCustomer = function (customerId) {
        $scope.ok = false;
        CustomerService.deleteCustomer(customerId)
          .then(function (ok) {
              $scope.ok = ok;
              $scope.searchByName();

              $scope.customer = '';
        });
    };
});

