module AOC.Day14 

open System
open System.Text.RegularExpressions

let parseTarget str = 
    let m = Regex.Match(str, "mem\[(\d+)\]")
    let cap = Array.tail [| for g in m.Groups -> g.Value |]
    int64 cap.[0]

let executeInstruction maskCreator addressDecoder valueAssigner (program, mask) (instruction: string)  =
    let splat = instruction.Split([|" = "|], StringSplitOptions.RemoveEmptyEntries)
    if splat.[0] = "mask" then 
        program, (maskCreator splat.[1])
    else
        let targets = parseTarget splat.[0] |> addressDecoder mask
        valueAssigner program targets mask (int64 splat.[1]), mask

let part1 data = 
    let assignValue memory addresses (ormask, andmask, _) value =
        let address = Seq.exactlyOne addresses 
        Map.add address ((value ||| ormask) &&& andmask) memory

    let decodeAddress _ target = [target]

    let createMask str = 
        let orer = str |> Seq.map (fun c -> match c with | 'X' -> '0' | _ -> c) |> String.Concat
        let ander = str |> Seq.map (fun c -> match c with | 'X' -> '1' | _ -> c) |> String.Concat
        (Convert.ToInt64(orer, 2)), (Convert.ToInt64(ander, 2)), Seq.empty
    
    let executor = executeInstruction createMask decodeAddress assignValue
    Seq.fold executor (Map.empty, (0L, 0L, Seq.empty)) data 
    |> fst
    |> Map.fold (fun state _ value -> state + value) 0L 

let countWithMask floater =
    let notfloat = ~~~floater
    let rem = floater % 2L
    let baseitr = if rem = 0L then floater else rem
    let rec counter count = 
        if count > floater then 
            None 
        elif (count &&& notfloat) = 0L then
            Some (count, (count + baseitr))
        else 
            counter (count + (count &&& notfloat))
    Seq.unfold counter 0L

let part2 data = 
    let assignValue memory addresses _ value = 
        Seq.fold (fun map address -> Map.add address value map) memory addresses

    let decodeAddress (ormask, xormask, offsets) target = 
        offsets |> Seq.map (fun x -> (((target ||| xormask) ^^^ xormask) ||| x) ||| ormask)

    let createMask str = 
        let orer = str |> Seq.map (fun c -> match c with | 'X' -> '0' | _ -> c) |> String.Concat
        let floater = str |> Seq.map (fun c -> match c with | '1' -> '0' | 'X' -> '1' | _ -> c) |> String.Concat
        let floatint = Convert.ToInt64(floater, 2)
        Convert.ToInt64(orer, 2), floatint, countWithMask floatint

    let executor = executeInstruction createMask decodeAddress assignValue
    Seq.fold executor (Map.empty, (0L, 0L, Seq.empty)) data 
    |> fst
    |> Map.fold (fun state _ value -> state + value) 0L 