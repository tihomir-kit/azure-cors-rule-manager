(function (ACRM, $) {
    ACRM.handleItemRemoval = function () {
        var index = $(this).data("index");
        var camelPrefix = $(this).data("camel-prefix");
        var dashPrefix = $(this).data("dash-prefix");

        removeItme(index, dashPrefix)
        recalculateIdentifiers(camelPrefix, dashPrefix)
    }

    function removeItme(index, dashPrefix) {
        var divSelector = "#" + dashPrefix + "-inner-holder-" + index;
        $(divSelector).remove();
    }

    function recalculateIdentifiers(camelPrefix, dashPrefix) {
        var inputClassSelector = "#" + dashPrefix + "-holder input";
        var aHrefClassSelector = "#" + dashPrefix + "-holder a";

        $(inputClassSelector).each(function (index, element) {
            var newId = camelPrefix + "_" + index + "_";
            var newName = camelPrefix + "[" + index + "]";
            element.setAttribute("id", newId);
            element.setAttribute("name", newName);
        });

        $(aHrefClassSelector).each(function (index, element) {
            element.setAttribute("data-index", index);
        });
    }
}(window.ACRM = window.ACRM || {}, jQuery));



$(document).ready(function () {
    $(".remove-button").on('click', ACRM.handleItemRemoval);
});