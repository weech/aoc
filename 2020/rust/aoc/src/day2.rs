fn extract_psk_and_rule(string: &str) -> (usize, usize, char, String) {
    let re = regex::Regex::new(r"(\d+)-(\d+) ([a-z]): (.+)").unwrap();
    let cap = re.captures(string).unwrap();
    (
        cap[1].parse().unwrap(),
        cap[2].parse().unwrap(),
        cap[3].chars().nth(0).unwrap(),
        cap[4].into(),
    )
}

fn part1(data: &[String]) -> usize {
    data.iter()
        .filter(|line| {
            let (min, max, letter, word) = extract_psk_and_rule(line);
            let letter_count = word.chars().filter(|x| *x == letter).count();
            letter_count >= min && letter_count <= max
        })
        .count()
}

fn part2(data: &[String]) -> usize {
    data.iter()
        .filter(|line| {
            let (first, last, letter, word) = extract_psk_and_rule(line);
            (word.chars().nth(first - 1).unwrap() == letter)
                != (word.chars().nth(last - 1).unwrap() == letter)
        })
        .count()
}

#[cfg(test)]
mod tests {
    #[test]
    fn day2_part1() {
        let testdata = [
            "1-3 a: abcde".to_string(),
            "1-3 b: cdefg".to_string(),
            "2-9 c: ccccccccc".to_string(),
        ];
        assert_eq!(2, crate::day2::part1(&testdata));
        assert_eq!(603, crate::day2::part1(&crate::parsers::day2()));
    }

    #[test]
    fn day2_part2() {
        let testdata = [
            "1-3 a: abcde".to_string(),
            "1-3 b: cdefg".to_string(),
            "2-9 c: ccccccccc".to_string(),
        ];
        assert_eq!(1, crate::day2::part2(&testdata));
        assert_eq!(404, crate::day2::part2(&crate::parsers::day2()));
    }
}
