using System.Collections.Generic;

namespace DataManager {
    public static class ComplaintBook {

        private static List<Answer> answers = new List<Answer>();
        public static Answer[] Answers => new List<Answer>(answers).ToArray(); // returns copy
        public static int Size => answers.Count;

        public static int Mismatches { get; private set; } = 0;

        public static void Reset() {
            answers = new List<Answer>();
            Mismatches = 0;
        }

        public static bool? MakeGuess(int rightId, int givenId) {

            answers.Add(new Answer(rightId, givenId));
            if (rightId != givenId) {
                Mismatches++;
                return givenId == -1 ? (bool?)null : false;
            }
            return true;
        }

        public struct Answer {
            public readonly int rightId;
            public readonly int givenId;

            public Answer(int rightId, int givenId) {
                this.rightId = rightId;
                this.givenId = givenId;
            }
        }
    }
}