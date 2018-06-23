TinySyntaxHighlighter["language-DOTHTML"] =
    {
        states: [
            {
                name: "default", style: "default",
                transitions: [
                    { expression: new RegExp("@"), state: "directive", style: "code_block" },
                    { expression: new RegExp("<!--"), state: "comment", style: "comment" },
                    { expression: new RegExp("<!\\[CDATA\\["), state: "cdata", style: "cdata" },
                    { expression: new RegExp("<%"), state: "code_block", style: "code_block" },
                    { expression: new RegExp("<"), state: "elm_name", style: "brace" },
                    { expression: new RegExp("\\{\\{"), state: "binding_name_double", style: "code_block" }
                ]
            },
            {
                name: "directive", style: "attr_value",
                transitions: [
                    { expression: new RegExp("\\n"), state: "default", style: "binding" }
                ]
            },
            {
                name: "default_body", style: "default",
                transitions: [
                    { expression: new RegExp("<!--"), state: "comment", style: "comment" },
                    { expression: new RegExp("<!\\[CDATA\\["), state: "cdata", style: "cdata" },
                    { expression: new RegExp("<%"), state: "code_block", style: "code_block" },
                    { expression: new RegExp("<"), state: "elm_name", style: "brace" },
                    { expression: new RegExp("\\{\\{"), state: "binding_name_double", style: "code_block" }
                ]
            },
            {
                name: "binding_name_double", style: "binding",
                transitions: [
                    { expression: new RegExp(":"), state: "binding_value_double", style: "binding" }
                ]
            },
            {
                name: "binding_value_double", style: "binding",
                transitions: [
                    { expression: new RegExp("\\}\\}"), state: "default", style: "code_block" }
                ]
            },
            {
                name: "code_block", style: "default",
                transitions: [
                    { expression: new RegExp("%>"), state: "default_body", style: "code_block" }
                ]
            },
            {
                name: "comment", style: "comment",
                transitions: [
                    { expression: new RegExp("-->"), state: "default_body", style: "comment" }
                ]
            },
            {
                name: "cdata", style: "cdata",
                transitions: [
                    { expression: new RegExp("\\]\\]>"), state: "default_body", style: "cdata" }
                ]
            },
            {
                name: "elm_name", style: "elm_name",
                transitions: [
                    { expression: new RegExp("\\s*/"), state: "elm_name", style: "brace" },
                    { expression: new RegExp("\\?"), state: "elm_name", style: "brace" },
                    { expression: new RegExp(">"), state: "default_body", style: "brace" },
                    { expression: new RegExp("\\s"), state: "attr_name", style: "elm_name" }
                ]
            },
            {
                name: "attr_name", style: "attr_name",
                transitions: [
                    { expression: new RegExp(">"), state: "default_body", style: "brace" },
                    { expression: new RegExp("=\\s*\""), state: "attr_value_q", style: "attr_quote" },
                    { expression: new RegExp("=\\s*'"), state: "attr_value_a", style: "attr_quote" }
                ]
            },
            {
                name: "attr_value_q", style: "attr_value",
                transitions: [
                    { expression: new RegExp("^\{"), state: "binding_name_q", style: "code_block" },
                    { expression: new RegExp("\""), state: "elm_name", style: "attr_quote" }
                ]
            },
            {
                name: "attr_value_a", style: "attr_value",
                transitions: [
                    { expression: new RegExp("^\{"), state: "binding_name_a", style: "code_block" },
                    { expression: new RegExp("'"), state: "elm_name", style: "attr_quote" }
                ]
            },
            {
                name: "binding_name_q", style: "binding",
                transitions: [
                    { expression: new RegExp(":"), state: "binding_value_q", style: "binding" }
                ]
            },
            {
                name: "binding_value_q", style: "binding",
                transitions: [
                    { expression: new RegExp("\\}"), state: "attr_value_q", style: "code_block" }
                ]
            },
            {
                name: "binding_name_a", style: "binding",
                transitions: [
                    { expression: new RegExp(":"), state: "binding_value_a", style: "binding" }
                ]
            },
            {
                name: "binding_value_a", style: "binding",
                transitions: [
                    { expression: new RegExp("\\}"), state: "attr_value_a", style: "code_block" }
                ]
            },
        ],
        styles: [
            {
                name: "default",
                style: { color: "black" }
            },
            {
                name: "comment",
                style: { color: "green" }
            },
            {
                name: "cdata",
                style: { color: "gray" }
            },
            {
                name: "brace",
                style: { color: "blue" }
            },
            {
                name: "elm_name",
                style: { color: "maroon" }
            },
            {
                name: "attr_name",
                style: { color: "red" }
            },
            {
                name: "attr_value",
                style: { color: "blue" }
            },
            {
                name: "binding",
                style: { color: "blue", background_color: "#e0e0e0" }
            },
            {
                name: "attr_quote",
                style: { color: "black" }
            },
            {
                name: "code_block",
                style: { color: "black", background_color: "yellow" }
            }
        ]
    };