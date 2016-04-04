customerApp.controller('CustomerController', function (CustomerService, $scope) {

    var messageHub = $.connection.customerHub;
    $.connection.hub.start();

    $scope.customers = [];

    $scope.searchResult = [];
    $scope.searchValue = '';
    $scope.ok = false;

    $scope.customerMessages = [];

    $scope.returnTotalSearchResults = function () {
        return $scope.searchResult.length;
    };

    $scope.searchByName = function () {
        $scope.searchResult = [];
        if ($scope.searchValue.length > 1) {
            CustomerService.searchCustomer($scope.searchValue)
             .then(function (customers) {
                 _.forEach(customers.CustomerList, function (result) {
                     var searchResultExists = _.find($scope.searchResult, function (value) { return value.Id == result.Id; });
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
            $scope.customerMessages = [];

            $scope.customers.push({ Name: $scope.customer.Name, Id: $scope.customer.Id });
            $scope.ok = true;

            $scope.searchByName();
        });

        $scope.newCustomerName = '';
    };

    $scope.editThisCustomer = function (customerId) {
        $scope.customerMessages = [];
        $scope.ok = false;

        CustomerService.getCustomer(customerId)
          .then(function (cust) {
              $scope.customer = cust;
              $scope.listMessages(cust);
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
              $scope.customerMessages = [];
          });
    };

    $scope.listMessages = function () {
        CustomerService.listMessages($scope.customer)
         .then(function (customerMessages) {
             _.forEach(customerMessages, function (message) {
                 $scope.addMessageToList(message);
             });
         });
    };

    $scope.addMessageToList = function(message) {
        if (message.From.Id === $scope.customer.Id) {
            $scope.customerMessages.push({ text: message.Text, fromMe: true, from: message.From.Name, to: message.To });
        }
        if (message.To === $scope.customer.Name) {
            $scope.customerMessages.push({ text: message.Text, fromMe: false, from: message.From.Name, to: message.To });
        }
    }

    $scope.sendMessage = function () {
        $scope.newMessage.from = $scope.customer;
        CustomerService.saveMessage($scope.newMessage);
    };

    messageHub.client.receiveMessage = function (message) {
        $scope.addMessageToList(message);
        $scope.$apply();
    }
});

