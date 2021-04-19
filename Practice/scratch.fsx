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

    let s1 = k1 |> Seq.ofArray
    let s2 = k2 |> Seq.ofArray

    (s1, s2)
    ||> Seq.zip
    |> Seq.map andChar
    //|> Seq.ofList
    |> String.concat ""



combineKeys key1 key2



