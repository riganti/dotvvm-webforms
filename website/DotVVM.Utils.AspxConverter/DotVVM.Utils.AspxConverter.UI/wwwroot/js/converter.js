function showSuggestion(spanIndex) {
    // reset all current highlights
    var highlighted = document.getElementsByClassName("highlight");
    for (var i = 0; i < highlighted.length; i++) {
        highlighted.item(i).classList.remove("highlight");
    }

    // highlight current suggestion
    window.setTimeout(function () {
        var highlight = document.querySelector("span[data-index='" + spanIndex + "']");
        highlight.classList.add("highlight");
        if (!isElementInViewport(highlight)) {
            highlight.scrollIntoView();
        }
    }, 0);
}

function isElementInViewport(el) {
    var rect = el.getBoundingClientRect();
    var displayArea = document.querySelector(".scrollable-area > div").getBoundingClientRect();
    return (
        rect.top >= displayArea.top &&
        rect.left >= displayArea.left &&
        rect.bottom <= displayArea.bottom &&
        rect.right <= displayArea.right
    );
}

function copyCode(code, stripClassDeclaration = false) {
    if (stripClassDeclaration) {
        code = code.substring(code.indexOf("{") + 1);
        code = code.substring(0, code.lastIndexOf("}"));
        code = code.trim();
        code = code.replace(/\n    /mg, "\n");
    }

    navigator.clipboard.writeText(code);
}

function togglePanel(sender) {
    var panel = sender.closest(".panel");
    panel.classList.toggle("collapsed");
}