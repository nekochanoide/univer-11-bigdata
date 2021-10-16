import fsp from "fs/promises";
import fs from "fs";
import os from "os";

const randomBetween = (a, b) => {
    const range = b - a;
    return Math.round(Math.random() * range) + a;
}

const file = await fsp.readFile("./demon.txt_Ascii.txt", { encoding: "utf-8" });
const lines = file.split(/\r?\n/);
const linesCount = lines.length;
const m = process.argv[2];

const resFile = fs.createWriteStream("res.txt", { encoding: "utf-8" });
for (let i = 0; i < m; i++) {
    const lineNumber = randomBetween(0, linesCount - 1);
    resFile.write(lines[lineNumber] + os.EOL);
}
