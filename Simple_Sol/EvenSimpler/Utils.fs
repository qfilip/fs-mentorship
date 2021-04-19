module Utils

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
    let hashResult = combineKeys combinedKey hash
    hashResult = "0000"
