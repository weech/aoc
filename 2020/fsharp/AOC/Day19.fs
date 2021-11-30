module AOC.Day19  

open System

type Rule =
    | Link of int list
    | Multi of Rule list
    | Literal of char

let makeLink (rules: string) =
    Link (List.ofSeq (Seq.map int (rules.Split([|' '|], StringSplitOptions.RemoveEmptyEntries))))

let readRule (str: string) = 
    let nameAndRule = str.Split([|": "|], StringSplitOptions.RemoveEmptyEntries)
    let name = int nameAndRule.[0]
    if Seq.head nameAndRule.[1] = '"' then 
        name, Literal (Seq.exactlyOne (Seq.take 1 (Seq.skip 1 nameAndRule.[1])))
    else
        let rules = nameAndRule.[1].Split([|" | "|], StringSplitOptions.RemoveEmptyEntries)
        if 1 = Array.length rules then
            name, makeLink rules.[0]
        else
            name, Multi (List.ofSeq (Seq.map makeLink rules))

let rec checkMatch rules message rule = 
    printfn "Checking %A against %A" message rule
    if List.isEmpty message then 
        match rule with 
        | Link l -> if not (List.isEmpty l) then printfn "Filtered %A" l
                    (List.length l <= 0), message 
        | _ -> false, message // Shouldn't happen
    else
        match rule with 
        | Literal c -> if c = List.head message then true, List.skip 1 message
                       else false, message
        | Multi l -> let res1 = checkMatch rules message (l.[0])
                     if fst res1 then res1
                     else checkMatch rules message (l.[1])
        | Link l -> match l with 
                    | [] -> true, message
                    | r1::rest -> let res = checkMatch rules message (Map.find r1 rules)
                                  printfn "%d returned %b for %A" r1 (fst res) message
                                  if fst res then 
                                    if List.isEmpty rest then res 
                                    else checkMatch rules (snd res) (Link rest)
                                  else false, message

let fullMatch rules message rule = 
    let res = checkMatch rules message rule 
    printfn "Match status for %s: %b + %A" (String.Concat(Array.ofList message)) (fst res) (snd res)
    (fst res) && (List.isEmpty (snd res))

let part1 (rules, messages) = 
    let parsedRules = Map (Seq.map readRule rules)
    Seq.sumBy (fun message -> if fullMatch parsedRules (List.ofSeq message) parsedRules.[0] then 1 else 0) messages

let part2 (rules, messages) = 
    let parsedRules = Map (Seq.map readRule rules)
    let parsedRules = Map.add 8 (Multi [Link [42]; Link [42; 8]]) parsedRules
    let parsedRules = Map.add 11 (Multi [Link [42; 31]; Link [42; 11; 31]]) parsedRules
    //let messages = ["aaaabbaaaabbaaa"] // Should be false
    let messages = ["aaaaabbaabaaaaababaa"] // Should be true
    Seq.sumBy (fun message -> if fullMatch parsedRules (List.ofSeq message) parsedRules.[0] then 1 else 0) messages
