module AOC.Day16 
// Not complete
open System
open System.Text.RegularExpressions

let determineRanges rules = 
    Map (Seq.map (fun rule -> 
        let m = Regex.Match(rule, "(.+?)(?=:): (\d+)-(\d+) or (\d+)-(\d+)")
        let cap = Array.tail [| for g in m.Groups -> g.Value |]
        let name = cap.[0].Trim ()
        let range1 = seq { (int cap.[1]) .. (int cap.[2]) }
        let range2 = seq { (int cap.[3]) .. (int cap.[4]) }
        let vals = Set.ofSeq (Seq.append range1 range2)
        name, vals
    ) rules)

let values mapping = mapping |> Map.toSeq |> Seq.map (fun (_, value) -> value)

let testMatch rules item = Seq.exists (Set.contains item) (values rules)

let part1 data = 
    let lined = Array.map (fun (x: string) -> x.Split([|'\n'|])) data 
    let rules = determineRanges(lined.[0])
    Array.sumBy (fun (ticket: string) ->
                 (ticket.Split([|','|], StringSplitOptions.RemoveEmptyEntries)
                 |> Seq.map int
                 |> Seq.sumBy (fun entry -> if testMatch rules entry then 0 else entry)))
                 lined.[2].[1..]

let findPossibleRules grid idx rules = 
    Map.fold (fun state key range -> 
              if Seq.forall (fun (row: int[]) -> Set.contains row.[idx] range) grid then
                state @ [key]
              else 
                state) 
            List.empty
            rules

let cleanDuplicates assigned possibles = 
    Map.map (fun _ matches -> List.except (Seq.filter (fun m -> Map.containsKey m assigned) matches) matches) possibles

let growAssigned assigned possibles = 
    Map.fold (fun state index matches -> 
                if List.length matches = 1 then 
                    Map.add (List.head matches) index state
                else 
                    state) assigned possibles

let rec filterPossibles assigned possibles = 
    if not (Seq.exists (fun (_, v) -> (List.length v) > 1) (Map.toSeq possibles)) then 
        assigned
    else
        let cleaned = cleanDuplicates assigned possibles 
        filterPossibles (growAssigned assigned cleaned) cleaned

let identifyColumns data = 
    let lined = Array.map (fun (x: string) -> x.Split([|'\n'|], StringSplitOptions.RemoveEmptyEntries)) data 
    let rules = determineRanges(lined.[0]) 
    let tickets = 
        Seq.choose (fun (ticket: string) -> 
                    let iticket = 
                        ticket.Split([|','|], StringSplitOptions.RemoveEmptyEntries)
                        |> Seq.map int 
                    if Seq.forall (testMatch rules) iticket then Some (Array.ofSeq iticket) else None)
                    lined.[2].[1..]
        |> List.ofSeq
    let assigned = 
        Seq.map (fun column -> column, findPossibleRules tickets column rules) 
                (seq {0 .. (Array.length (List.head tickets)) - 1})
        |> Map 
        |> (filterPossibles Map.empty)
    let mine = 
        lined.[1].[1].Split([|','|], StringSplitOptions.RemoveEmptyEntries)
        |> Array.map int  
    Map.map (fun _ value -> mine.[value]) assigned

let part2 data = 
    Map.fold (fun state (key: string) value -> 
                if key.Contains("departure") then state @ [int64 value] else state)
            List.empty 
            (identifyColumns data)
    |> List.fold (*) 1L