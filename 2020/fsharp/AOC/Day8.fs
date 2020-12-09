module AOC.Day8

open System 

type Program = 
    {Accumulator: int; Index: int}

    static member Default = 
        {Accumulator = 0; Index = 0}

type Instruction = 
    | Noop of number: int 
    | Jump of number: int 
    | Acc of number: int

type Status =
    | Success of acc: int
    | Failure of acc: int

let makeInstruction (str: string) = 
    let pieces = str.Split([|' '|])
    let number = int pieces.[1]
    match pieces.[0] with
    | "nop" -> Noop number
    | "jmp" -> Jump number
    | "acc" -> Acc number
    | _ -> invalidArg "str" "Not a valid instruction" 

let executeInstruction program instruction =
    match instruction with 
    | Noop _ -> {program with Index = program.Index + 1}
    | Jump n -> {program with Index = program.Index + n}
    | Acc n -> {Accumulator = program.Accumulator + n; Index = program.Index + 1}

let rec runTrial program data monitor = 
    if program.Index >= Array.length data then 
        Success program.Accumulator
    elif Set.contains program.Index monitor then 
        Failure program.Accumulator 
    else 
        runTrial (executeInstruction program data.[program.Index]) 
                data 
                (Set.add program.Index monitor)

let part1 data = 
    let parsedData = data |> Seq.map makeInstruction |> Array.ofSeq
    match runTrial Program.Default parsedData Set.empty with
    | Success acc -> acc // Won't happen
    | Failure acc -> acc

let swapInstruction instruction = 
    match instruction with 
    | Noop n -> Jump n 
    | Jump n -> Noop n 
    | Acc n -> Acc n 

let rec swapTrial (data: Instruction []) idx =
    let result = match data.[idx] with 
                    | Noop n -> (data.[idx] <- Jump n
                                 runTrial Program.Default data Set.empty)
                    | Jump n -> (data.[idx] <- Noop n
                                 runTrial Program.Default data Set.empty)
                    | Acc n -> Failure 0 // Ugly hack
    match result with 
    | Success n -> n
    | Failure _ -> (data.[idx] <- swapInstruction data.[idx]
                    swapTrial data (idx + 1))

let part2 data = 
    let parsedData = data |> Seq.map makeInstruction |> Array.ofSeq
    swapTrial parsedData 0