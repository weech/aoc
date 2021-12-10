module AOC.Day10

let isLeft bracket =
    match bracket with
    | '(' -> true
    | '[' -> true
    | '{' -> true
    | '<' -> true
    | _ -> false

let getRight bracket =
    match bracket with
    | '(' -> ')'
    | '[' -> ']'
    | '{' -> '}'
    | '<' -> '>'
    | _ -> invalidArg "bracket" "Not a left bracket"

let getPointsInvalid bracket =
    match bracket with
    | ')' -> 3
    | ']' -> 57
    | '}' -> 1197
    | '>' -> 25137
    | _ -> invalidArg "bracket" "Not a right bracket"


let rec checkCorrupted stack input =
    match input with
    | [] -> 0
    | nextChar :: rest ->
        if isLeft nextChar then
            checkCorrupted (nextChar :: stack) rest
        else
            let previous = List.head stack

            if getRight previous <> nextChar then
                getPointsInvalid nextChar
            else
                checkCorrupted (List.removeAt 0 stack) rest


let part1 data =
    data
    |> Seq.map (List.ofSeq >> (checkCorrupted List.empty))
    |> Seq.sum

let getPointsMissing bracket =
    match bracket with
    | '(' -> 1uL
    | '[' -> 2uL
    | '{' -> 3uL
    | '<' -> 4uL
    | _ -> invalidArg "bracket" "Not a right bracket"

// I could probably find a way to avoid building the stack twice, but I don't care enough
let rec buildStack stack input =
    match input with
    | [] -> stack
    | nextChar :: rest ->
        if isLeft nextChar then
            buildStack (nextChar :: stack) rest
        else
            // Always fine since we've filtered out corrupted rows
            buildStack (List.removeAt 0 stack) rest

let indexMiddle scores =
    let array = scores |> Seq.sort |> Array.ofSeq
    array.[(Array.length array) / 2]

let part2 data =
    data
    |> Seq.map List.ofSeq
    |> Seq.filter (checkCorrupted List.empty >> ((=) 0))
    |> Seq.map (
        (buildStack List.empty)
        >> Seq.fold (fun state b -> state * 5uL + getPointsMissing b) 0uL
    )
    |> indexMiddle
