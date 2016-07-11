function ValidationResult() {
    var _messages = [];

    return {
        add: add,
        getList: getList,
        getString: getString,
        isValid: isValid,
    };

    function add(message) {
        _messages.push(message);
    }

    function getList() {
        return _messages;
    }

    function getString() {
        var result = "";

        _.each(_messages, function (message) {
            result += "- " + message + "\n";
        });

        return result;
    }

    function isValid() {
        return _messages.length == 0;
    }
};