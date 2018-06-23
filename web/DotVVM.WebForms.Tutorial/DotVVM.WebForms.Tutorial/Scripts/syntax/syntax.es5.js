"use strict";

var TinySyntaxHighlighter = {
    currentStyle: "",

    /* Searches all CODE elements in a current page and highlights the syntax */
    initialize: function initialize() {

        // browse all CODE elements in the page
        var preElements = document.getElementsByTagName("code");
        for (var i = 0; i < preElements.length; i++) {

            // try highlight the syntax
            TinySyntaxHighlighter.highlightBlock(preElements[i]);
        }
    },

    /* Highlights a syntax in a given PRE element */
    highlightBlock: function highlightBlock(preElement) {

        // determine the language
        var m = preElement.className.match("language-([a-zA-Z]+)");
        if (m != null && m.length === 2) {
            if (TinySyntaxHighlighter[preElement.className]) {

                // syntax definition was found, highlight it
                preElement.innerHTML = TinySyntaxHighlighter.highlightSyntax(preElement.textContent || preElement.innerText, TinySyntaxHighlighter[preElement.className]);
            }
        }
    },

    /* Highlights */
    highlightSyntax: function highlightSyntax(source, definition) {

        // initialize
        this.currentStyle = "";
        var state = definition.states[0];
        var styleName = definition.styles[0].name;
        var text = [];
        TinySyntaxHighlighter.openStyle(text, definition, state.style);

        // process the source code
        for (var i = 0; i < source.length;) {

            // try to find a nearest applicable transition (first found wins)
            var transition = null;
            var index = -1;
            var m = null;
            var bestMatch = null;
            var rest = source.substring(i);
            for (var j in state.transitions) {
                if (j !== "asLinq") {
                    if ((m = rest.match(state.transitions[j].expression)) != null) {
                        if (index == -1 || m.index < index) {
                            transition = state.transitions[j];
                            bestMatch = m;
                            index = m.index;
                        }
                    }
                }
            }

            // change the state if needed
            if (transition) {
                // write a prefix and a recognition sequence
                text.push(TinySyntaxHighlighter.htmlEncode(source.substring(i, i + index)));
                TinySyntaxHighlighter.openStyle(text, definition, transition.style);
                text.push(TinySyntaxHighlighter.htmlEncode(bestMatch[0]));
                i = i + index + bestMatch[0].length;

                // change the state
                state = null;
                for (var j in definition.states) {
                    if (definition.states[j].name == transition.state) {
                        state = definition.states[j];
                        break;
                    }
                }
                if (state == null) throw "Invalid state name specified in a syntax definition!";

                // change the style
                TinySyntaxHighlighter.openStyle(text, definition, state.style);
            } else {
                // no applicable transition, enter the rest of a string
                text.push(TinySyntaxHighlighter.htmlEncode(source.substring(i)));
                i = source.length;
            }
        }

        // return the text
        TinySyntaxHighlighter.openStyle(text, definition, "");
        return text.join("");
    },

    /* Append a SPAN tag with a specified style definition */
    openStyle: function openStyle(text, definition, styleName) {

        // if there is no need to change the style, skip
        if (this.currentStyle == styleName) return;

        // close a style if it is open
        if (this.currentStyle != "") {
            this.currentStyle = "";
            text.push("</span>");
        }

        // find a specified style definition
        for (var i = 0; i < definition.styles.length; i++) {
            var style = definition.styles[i];
            if (style.name == styleName) {

                // append a SPAN tag
                var txt = "<span";
                if (style.style) {
                    txt = txt + " style=\"";
                    for (var j in style.style) txt = txt + j.replace(/_/g, "-") + ":" + style.style[j] + ";";
                    txt = txt + "\"";
                }
                txt = txt + ">";

                this.currentStyle = styleName;
                text.push(txt);
                return;
            }
        }

        if (styleName != "") throw "Invalid style name specified in a syntax definition!";
    },

    htmlEncode: function htmlEncode(str) {
        return str.replace(/&/g, "&amp;").replace(new RegExp("\"", "g"), "&quot;").replace(/</g, "&lt;").replace(/>/g, "&gt;").replace(/\r\n/g, "<br />").replace(/\n\r/g, "<br />").replace(/\n/g, "<br />").replace(/\r/g, "<br />");
    }
};

