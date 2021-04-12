#r "nuget: Thoth.Json.Net, 5.0.0"
#load "simpleTypes.fsx"

open Thoth.Json.Net
open System.IO

open SimpleTypes
open SimpleTypes.Age
open SimpleTypes.String5

type Role = Admin | User | Guest
type Code =
    | Digits of int
    | Hash of String5

type User = {
    Name: string
    Age: Age
    Role: Role
    Email: string option
    Code: Code
}

let filepath = "D:\\Riznice\\fs-mentorship\\Practice\\sample.json"


let writeToFile content =
    File.WriteAllText (filepath, content)


let readFromFile = File.ReadAllText filepath

// verify codeString & ageInt with bind???

let makeUser code age =
    match age with
    | Error e -> printfn "%s" e
    | Ok a ->
        let user = { Name = "Bob"; Age = a; Role = Admin; Email = None; Code = code }
        let json = Encode.Auto.toString(4, user)
        writeToFile json |> ignore

        let jsonText = readFromFile
        let userOfJson = Decode.Auto.fromString<User>(jsonText)

        printfn "%A" userOfJson



let codeStr = String5.wrap "IDDQD"
match codeStr with
| Error e -> ()
| Ok hash ->
    let code = Hash hash
    let age = Age.wrap 27
    makeUser code age


