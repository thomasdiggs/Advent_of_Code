using System.Text;

namespace Day_7
{
    class Hand
    {
        public Card[] Cards { get; }
        public int Bid { get; }
        public HandType HandType { get; }
        public long Value { get; set; } = 0;
        public int Rank { get; set; } = 0;

        public Hand(char[] cards, int bid)
        {
            Cards = ConvertCharToCardArray(cards);
            Bid = bid;
            HandType = UpdateHandType();
            Value = SetValue();
        }

        /// <summary>
        /// Sets the value of the hand based on the cards and the hand type.
        /// This value is unique for each hand and is used to rank the hands.
        /// As long as there are no duplicte cards and hand types, the value will be unique.
        /// Take the two digits of hand type and append the two digits of each card.
        /// Example: 32T3K 765 -> 010203030511
        /// Where 01 is the hand type, 02 is the first card, 03 is the second card, 03 is the third card, 05 is the fourth card, 11 is the fifth card.
        /// This is according to the enum values of HandType and Card.
        /// </summary>
        /// <returns></returns>
        private long SetValue()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(((int)HandType).ToString("00"));
            foreach (Card card in Cards)
            {
                sb.Append(((int)card).ToString("00"));
            }
            return long.Parse(sb.ToString());
        }

        /// <summary>
        /// Updates the hand type based on the cards.
        /// Notice that for each hand type, the number of duplicate cards is unique.
        /// A HighCard has no duplicate cards, a OnePair has one duplicate card, a TwoPair has two duplicate cards, etc.
        /// </summary>
        /// <returns></returns>
        private HandType UpdateHandType()
        {
            if (Cards.Length == 5)
            {
                int conversionNumber = 0;
                for (int i = 0; i < Cards.Length; i++)
                {
                    for (int j = i + 1; j < Cards.Length; j++)
                    {
                        if (Cards[i] == Cards[j])
                        {
                            conversionNumber++;
                        }
                    }
                }
                return conversionNumber switch
                {
                    0 => HandType.HighCard,
                    1 => HandType.OnePair,
                    2 => HandType.TwoPair,
                    3 => HandType.ThreeOfAKind,
                    4 => HandType.FullHouse,
                    6 => HandType.FourOfAKind,
                    10 => HandType.FiveOfAKind,
                    _ => HandType.Error,
                };
            }
            return HandType.Unknown;
        }

        private Card[] ConvertCharToCardArray(char[] cards)
        {
            Card[] cardArray = new Card[cards.Length];
            for (int i = 0; i < cards.Length; i++)
            {
                cardArray[i] = cards[i] switch
                {
                    '2' => Card.Two,
                    '3' => Card.Three,
                    '4' => Card.Four,
                    '5' => Card.Five,
                    '6' => Card.Six,
                    '7' => Card.Seven,
                    '8' => Card.Eight,
                    '9' => Card.Nine,
                    'T' => Card.Ten,
                    'J' => Card.Jack,
                    'Q' => Card.Queen,
                    'K' => Card.King,
                    'A' => Card.Ace,
                    _ => Card.Error,
                };
            }
            return cardArray;
        }

        override public string ToString()
        {
            return $"""
                Cards:  {string.Join("", Cards)}
                Bid:    {Bid}
                Type:   {HandType}
                Rank:   {Rank}
                Value:  {Value}

                """;
        }
    }

    enum HandType
    {
        HighCard,
        OnePair,
        TwoPair,
        ThreeOfAKind,
        FullHouse,
        FourOfAKind,
        FiveOfAKind,
        Unknown = -1,
        Error = -2
    }

    enum Card
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace,
        Error = -2
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

            string[] input = [
                "32T3K 765",
                "T55J5 684",
                "KK677 28",
                "KTJJT 220",
                "QQQJA 483"
            ];

            List<Hand> hands = [];

            foreach (string line in input)
            {
                string cards = line.Split(' ')[0];
                string bid = line.Split(' ')[1];
                hands.Add(new Hand(cards.ToCharArray(), int.Parse(bid)));
            }

            hands = hands.OrderBy(hand => hand.Value).ToList();

            int totalWinnings = 0;
            for (int i = 0; i < hands.Count; i++)
            {
                hands[i].Rank = i + 1;
                totalWinnings += hands[i].Bid * hands[i].Rank;
            }

            Console.WriteLine($"Total winnings: {totalWinnings}");
        }
    }
}
