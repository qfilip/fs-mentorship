type Direction = North | South | East

let (|South|_|) x = if x = South then Some x else None

let testDir d = function
   | North -> printfn ""
   | South -> printfn ""
   | East -> printfn ""

