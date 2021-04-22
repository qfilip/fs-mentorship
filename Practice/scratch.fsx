open System


let key1 = "0101"
let key2 = "0110"

let charToInt c = int c - int '0'


let combineKeys k1 k2 =
    let andChar (zip: char * char) =
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


let decodeKey (key: string) =
    let rec computeIntValue bits bitValue currentValue =
        match bits with
        | [] -> currentValue
        | x::xs ->
            let xchar = x |> charToInt
            let nextValue = currentValue + (xchar * bitValue)
            let nextBitValue = bitValue * 2
            computeIntValue xs nextBitValue nextValue
            
    let bits = key |> Seq.toList |> List.rev
    
    (computeIntValue bits 1 0)