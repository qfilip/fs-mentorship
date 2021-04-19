module Utils
open System

[<Literal>]
let hashLength = 10

let charToInt c = int c - int '0'

let combineKeys key1 key2 =
    let binaryAndChars (zip: char * char) =
        let result = (fst zip |> charToInt) &&& (snd zip |> charToInt)
        result.ToString()

    // how to avoid turning Seq to list? // see below *1
    let l1 = key1 |> Seq.toList
    let l2 = key2 |> Seq.toList

    let result =
        (l1, l2) 
        ||> List.zip
        |> List.map binaryAndChars
        |> Seq.ofList // *1 - to avoid this line
        |> String.concat ""

    result


let varifyHash combinedKey hash =
    let validHash = 
        seq { 1..hashLength }
        |> Seq.map(fun x -> "0")
        |> String.concat ""
    
    let hashResult = combineKeys combinedKey hash
    
    hashResult = validHash


let toBinary number = 
    let sequence = List.empty

    let rec loop number increment state =
        match (state |> List.length = hashLength) with
        | true -> state
        | _ ->
            let bit = int (Math.Floor(decimal (number % 2)))
            let newNumber = number / 2
            let inc = increment * 2
            let newState = state @ [bit]
            loop newNumber inc newState

    loop number 1 sequence
    |> List.rev
    |> Seq.map(fun x -> x.ToString())
    |> String.concat ""
