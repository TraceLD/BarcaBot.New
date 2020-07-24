let data = require("./data.json");
let fs = require("fs");

let s = "private readonly Dictionary<string, string> _countries = new Dictionary<string, string>\n{\n";

data.forEach(el => {
    s += `    {\"${el.name}\", \"${el.alpha}\"},\n`;
});

s += "};";

fs.writeFile("./Out.cs", s, function(err) {
    if (err) {
        console.log(err);
    }
});