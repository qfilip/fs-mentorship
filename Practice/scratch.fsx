open System
// 101 5
// 110 6
//
// 001 1

let key1 = "0101"
let key2 = "0110"

let combineKeys k1 k2 =
    let andChar (zip: char * char) =
        let charToInt c = int c - int '0'
        let result = (fst zip |> charToInt) &&& (snd zip |> charToInt)
        result.ToString()

    let s1 = k1 |> Seq.toList
    let s2 = k2 |> Seq.toList

    (s1, s2)
    ||> Seq.zip
    |> Seq.map andChar
    // |> Seq.ofList
    |> String.concat ""

combineKeys key1 key2

let toBinary number = 
    let sequence = List.empty

    let rec loop number increment state =
        match (state |> List.length = 10) with
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

let x = toBinary 10


let generateValidHash =
    let hashLength = 10
    seq {1..hashLength}
    |> Seq.map(fun x -> "0")
    |> String.concat ""

