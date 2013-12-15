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
        var divClassSelector = "." + dashPrefix + "-inner-holder";
        var inputClassSelector = divClassSelector + " input";
        var buttonClassSelector = divClassSelector + " button";

        $(divClassSelector).each(function (index, element) {
            var newId = "#" + dashPrefix + "-holder-" + index;
            element.setAttribute("id", newId);
        });

        $(inputClassSelector).each(function (index, element) {
            var newId = camelPrefix + "_" + index + "_";
            var newName = camelPrefix + "[" + index + "]";
            element.setAttribute("id", newId);
            element.setAttribute("name", newName);
        });

        $(buttonClassSelector).each(function (index, element) {
            element.setAttribute("data-index", index);
        });
    }

    ACRM.handleItemAppend = function () {
        var camelPrefix = $(this).data("camel-prefix");
        var dashPrefix = $(this).data("dash-prefix");

        appendInnerDivHolder(camelPrefix, dashPrefix);
        recalculateIdentifiers(camelPrefix, dashPrefix);
    }

    function appendInnerDivHolder(camelPrefix, dashPrefix) {
        var divHolderId = "#" + dashPrefix + "-holder";

        var innerHolder = $("<div/>", {
            class: dashPrefix + "-inner-holder"
        });

        var input = $("<input/>", {
            class: dashPrefix + "-box",
            type: "text"
        });

        var button = $("<button/>", {
            class: "add-button",
            type: "button",
            "data-camel-prefix": camelPrefix,
            "data-dash-prefix": dashPrefix
        });

        $(innerHolder).append(input);
        $(innerHolder).append(button);
        $(divHolderId).append(innerHolder);
    }
}(window.ACRM = window.ACRM || {}, jQuery));



$(document).ready(function () {
    $(".remove-button").on('click', ACRM.handleItemRemoval);
    $(".add-button").on('click', ACRM.handleItemAppend);
});

