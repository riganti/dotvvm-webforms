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
    return (
        rect.top >= 0 &&
            rect.left >= 0 &&
            rect.bottom <= (window.innerHeight || document.documentElement.clientHeight) &&
            rect.right <= (window.innerWidth || document.documentElement.clientWidth)
    );
}

function copyCode(code) {
    navigator.clipboard.writeText(code);
}

function togglePanel(sender) {
    var panel = sender.closest(".panel");
    panel.classList.toggle("collapsed");
}