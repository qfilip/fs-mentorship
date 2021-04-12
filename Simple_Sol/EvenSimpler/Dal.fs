module Dal

open System.IO

module Common =
    let private filepath = Path.Combine(__SOURCE_DIRECTORY__, "evenSimplerDB.json")
    let private writeToFile content = File.WriteAllText (filepath, content)
    let private readFromFile = File.ReadAllText filepath



module Mock =
    let mockit = ()