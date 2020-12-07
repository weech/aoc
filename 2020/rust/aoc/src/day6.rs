use std::collections::HashSet;

fn settify_string(person: &str) -> HashSet<char> {
    person.chars().collect()
}

fn full_set() -> HashSet<char> {
    ('a'..='z').collect()
}

fn part1(data: &[Vec<String>]) -> usize {
    data.iter()
        .map(|group| {
            group
                .iter()
                .fold("".to_string(), |acc, person| format!("{}{}", acc, person))
                .chars()
                .collect::<HashSet<_>>()
                .len()
        })
        .sum()
}

fn part2(data: &[Vec<String>]) -> usize {
    data.iter()
        .map(|group| {
            dbg!(group);
            dbg!(group.iter().fold(full_set(), |acc, ele| {
                acc.intersection(&ele.chars().collect()).copied().collect()
            }))
            .len()
        })
        .sum()
}

#[cfg(test)]
mod tests {
    #[test]
    fn day6_part1() {
        let data = [
            vec!["abc".to_string()],
            vec!["a".to_string(), "b".to_string(), "c".to_string()],
            vec!["ab".to_string(), "ac".to_string()],
            vec![
                "a".to_string(),
                "a".to_string(),
                "a".to_string(),
                "a".to_string(),
            ],
            vec!["b".to_string()],
        ];
        assert_eq!(11, crate::day6::part1(&data));
        assert_eq!(6161, crate::day6::part1(&crate::parsers::day6()));
    }
    #[test]
    fn day6_part2() {
        let data = [
            vec!["abc".to_string()],
            vec!["a".to_string(), "b".to_string(), "c".to_string()],
            vec!["ab".to_string(), "ac".to_string()],
            vec![
                "a".to_string(),
                "a".to_string(),
                "a".to_string(),
                "a".to_string(),
            ],
            vec!["b".to_string()],
        ];
        assert_eq!(6, crate::day6::part2(&data));
        assert_eq!(2971, crate::day6::part2(&crate::parsers::day6()));
    }
}
