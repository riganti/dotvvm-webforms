"use strict";

TinySyntaxHighlighter["language-CSHARP"] = {
    states: [{
        name: "default", style: "default",
        transitions: [{ expression: new RegExp("@\""), state: "string_ml", style: "string" }, { expression: new RegExp("\""), state: "string", style: "string" }, { expression: new RegExp("'"), state: "char", style: "string" }, { expression: new RegExp("///"), state: "comment", style: "comment_xml" }, { expression: new RegExp("//"), state: "comment", style: "comment" }, { expression: new RegExp("/\\*"), state: "comment_ml", style: "comment" }, { expression: new RegExp("\\b(abstract|as|base|bool|break|byte|case|catch|char|checked|class|const|continue|decimal|default|delegate|do|double|else|enum|event|explicit|extern|false|finally|fixed|float|for|foreach|nameof|goto|if|implicit|in|int|interface|internal|is|lock|long|namespace|new|null|object|operator|out|override|params|private|protected|public|readonly|ref|return|sbyte|sealed|short|sizeof|stackalloc|static|string|struct|switch|this|throw|true|try|typeof|uint|ulong|unchecked|unsafe|ushort|using|virtual|void|volatile|while|dynamic|from|get|global|group|into|join|let|orderby|partial|remove|select|set|value|var|where|where|yield)\\b", "i"), state: "default", style: "keyword" }]
    }, {
        name: "comment", style: "comment",
        transitions: [{ expression: new RegExp("<[/a-zA-Z0-9]"), state: "comment_xml", style: "comment_xml" }, { expression: new RegExp("\\n"), state: "default", style: "comment" }]
    }, {
        name: "comment_xml", style: "comment_xml",
        transitions: [{ expression: new RegExp(">"), state: "comment", style: "comment_xml" }]
    }, {
        name: "comment_ml", style: "comment",
        transitions: [{ expression: new RegExp("\\*/"), state: "default", style: "comment" }]
    }, {
        name: "string_ml", style: "string",
        transitions: [{ expression: new RegExp("\"\""), state: "string_ml", style: "string" }, { expression: new RegExp("\""), state: "default", style: "string" }]
    }, {
        name: "string", style: "string",
        transitions: [{ expression: new RegExp("\""), state: "default", style: "string" }, { expression: new RegExp("\\n"), state: "default", style: "string" }, { expression: new RegExp("\\\\\""), state: "string", style: "string" }]
    }, {
        name: "char", style: "string",
        transitions: [{ expression: new RegExp("\\\\'"), state: "char", style: "string" }, { expression: new RegExp("'"), state: "default", style: "string" }]
    }],
    styles: [{
        name: "default",
        style: { color: "black" }
    }, {
        name: "comment",
        style: { color: "green" }
    }, {
        name: "comment_xml",
        style: { color: "gray" }
    }, {
        name: "string",
        style: { color: "maroon" }
    }, {
        name: "keyword",
        style: { color: "blue" }
    }, {
        name: "class_name",
        style: { color: "#2b91af" }
    }]
};

