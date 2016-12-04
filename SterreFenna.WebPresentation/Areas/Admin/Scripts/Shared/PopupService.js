var PopupService = function () {
    var _popupHtml = $("#popupModal");
    var _onConfirm = null;

    return {
        setTitle: setTitle,
        setBody: setBody,
        setConfirmButtonText, setConfirmButtonText,
        onConfirm: onConfirm,
        onSaveClick: onSaveClick,
        show: show,
        confirmationButtonIsDeleteButton: confirmationButtonIsDeleteButton,
        confirmationButtonIsDefaultButton: confirmationButtonIsDefaultButton,
    };

    function setTitle(title) {
        _popupHtml.html(_popupHtml.html().replace("{title}", title));
    }

    function setBody(body) {
        _popupHtml.html(_popupHtml.html().replace("{body}", body));
    }

    function setConfirmButtonText(caption) {
        _popupHtml.html(_popupHtml.html().replace("{confirmButtonText}", caption));
    }

    function confirmationButtonIsDeleteButton() {
        _popupHtml.html(_popupHtml.html().replace("{confirmCssClass}", "btn-danger"));
    }

    function confirmationButtonIsDefaultButton() {
        _popupHtml.html(_popupHtml.html().replace("{confirmCssClass}", "btn-primary"));
    }

    function onConfirm(action) {
        _onConfirm = action;
    }

    function show() {
        _popupHtml.modal('toggle');
    }

    function onSaveClick() {
        if (_onConfirm != null)
            _onConfirm();
    }
}();