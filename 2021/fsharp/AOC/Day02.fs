module AOC.Day02

type Instruction =
    | Forward of int 
    | Down of int 
    | Up of int

type Location = {Depth: int; Position: int; Aim: int}

let makeInstruction (phrase: string) =
    let words = phrase.Split(' ')
    let count = int words[1]
    if words[0] = "forward" then 
        Forward count
    elif words[0] = "down" then 
        Down count 
    else 
        Up count 

let moveNaive current instruction = 
    match instruction with 
    | Forward x -> { current with Position = current.Position + x }
    | Down x -> { current with Depth = current.Depth + x }
    | Up x -> { current with Depth = current.Depth - x }

let moveSophisticated current instruction = 
    match instruction with 
    | Forward x -> { current with Position = current.Position + x; Depth = current.Depth + current.Aim * x }
    | Down x -> { current with Aim = current.Aim + x }
    | Up x -> { current with Aim = current.Aim - x }

let formatOutput location = 
    location.Depth * location.Position

let part1 data = 
    data
    |> Seq.fold (fun state ele -> moveNaive state (makeInstruction ele)) {Depth = 0; Position = 0; Aim = 0}
    |> formatOutput 

let part2 data = 
    data
    |> Seq.fold (fun state ele -> moveSophisticated state (makeInstruction ele)) {Depth = 0; Position = 0; Aim = 0}
    |> formatOutput 