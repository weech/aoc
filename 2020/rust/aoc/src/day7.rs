use std::collections::{HashMap, HashSet, VecDeque};

struct Rule {
    count: usize,
    color: String,
}

fn parse_bag(line: &String) -> (String, Vec<Rule>) {
    let words: Vec<_> = line.split(' ').collect();
    let color = words[0..2].join(" ");
    let bag_half_whole = words[4..].join(" ");
    let mut bag_half = bag_half_whole.split(',').peekable();
    if *bag_half.peek().unwrap() == "no other bags." {
        (color, Vec::new())
    } else {
        let bags = bag_half.map(|rule| {
            let mut rwords = rule.split(' ').filter(|s| s.trim() != "");
            let count: usize = rwords.next().unwrap().parse().unwrap();
            let rcolor = rwords.take(2).collect::<Vec<_>>().join(" ");
            Rule {
                count,
                color: rcolor,
            }
        });
        (color, bags.collect())
    }
}

fn gather_parents<'a, 'b>(
    bags: &'a [(String, Vec<Rule>)],
    desire: &'b str,
) -> VecDeque<&'a String> {
    let mut parent_colors: VecDeque<_> = bags
        .iter()
        .filter_map(|(color, rules)| {
            if rules.iter().any(|rule| rule.color == desire) {
                Some(color)
            } else {
                None
            }
        })
        .collect();
    parent_colors.extend(
        parent_colors
            .clone()
            .iter()
            .map(|color| gather_parents(bags, color))
            .flatten(),
    );
    parent_colors
}

// Currently > 1 ms
pub fn part1(data: &[String]) -> usize {
    let desire = "shiny gold";
    let bags: Vec<_> = data.iter().map(parse_bag).collect();
    gather_parents(&bags, desire)
        .iter()
        .collect::<HashSet<_>>()
        .len()
}

fn count_children(bags: &HashMap<String, Vec<Rule>>, desire: &str) -> usize {
    bags[desire]
        .iter()
        .map(|rule| rule.count + (rule.count * count_children(bags, &rule.color)))
        .sum()
}

pub fn part2(data: &[String]) -> usize {
    let desire = "shiny gold";
    let bags: HashMap<_, _> = data.iter().map(parse_bag).collect();
    count_children(&bags, desire)
}

#[cfg(test)]
mod tests {
    #[test]
    fn day7_part1() {
        let data = [
            "light red bags contain 1 bright white bag, 2 muted yellow bags.".to_string(),
            "dark orange bags contain 3 bright white bags, 4 muted yellow bags.".to_string(),
            "bright white bags contain 1 shiny gold bag.".to_string(),
            "muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.".to_string(),
            "shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.".to_string(),
            "dark olive bags contain 3 faded blue bags, 4 dotted black bags.".to_string(),
            "vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.".to_string(),
            "faded blue bags contain no other bags.".to_string(),
            "dotted black bags contain no other bags.".to_string(),
        ];
        assert_eq!(4, crate::day7::part1(&data));
        assert_eq!(148, crate::day7::part1(&crate::parsers::day7()));
    }
    #[test]
    fn day7_part2() {
        let data = [
            "light red bags contain 1 bright white bag, 2 muted yellow bags.".to_string(),
            "dark orange bags contain 3 bright white bags, 4 muted yellow bags.".to_string(),
            "bright white bags contain 1 shiny gold bag.".to_string(),
            "muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.".to_string(),
            "shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.".to_string(),
            "dark olive bags contain 3 faded blue bags, 4 dotted black bags.".to_string(),
            "vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.".to_string(),
            "faded blue bags contain no other bags.".to_string(),
            "dotted black bags contain no other bags.".to_string(),
        ];
        assert_eq!(32, crate::day7::part2(&data));
        let data2 = [
            "shiny gold bags contain 2 dark red bags.".to_string(),
            "dark red bags contain 2 dark orange bags.".to_string(),
            "dark orange bags contain 2 dark yellow bags.".to_string(),
            "dark yellow bags contain 2 dark green bags.".to_string(),
            "dark green bags contain 2 dark blue bags.".to_string(),
            "dark blue bags contain 2 dark violet bags.".to_string(),
            "dark violet bags contain no other bags.".to_string(),
        ];
        assert_eq!(126, crate::day7::part2(&data2));
        assert_eq!(24867, crate::day7::part2(&crate::parsers::day7()));
    }
}
