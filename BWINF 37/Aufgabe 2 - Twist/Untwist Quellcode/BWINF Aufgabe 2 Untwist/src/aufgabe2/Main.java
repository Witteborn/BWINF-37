package aufgabe2;

public class Main {
	public static void main(String[] args) {

		java.util.Scanner scan = new java.util.Scanner(System.in);
		System.out.println("use your own wordlist? [True/False]");
		boolean useOwnWordList = false;
		try {
			useOwnWordList = scan.nextBoolean();
			if (useOwnWordList) {
				System.out.println("Filename of the Wordlist: (ex. woerterliste.txt)");
				String FilePath = scan.next();
				Untwist.wordlist = Untwist.readWordlist(FilePath);
			}
		} catch (java.util.InputMismatchException ime) {
		}
		if (!useOwnWordList) {
			boolean alsoCheckForEnglish = checkForEnglish();

			System.out.println("Loading wordlist!");
			Untwist.wordlist = Untwist.readWordlist(alsoCheckForEnglish);
			System.out.println("Loading done!");
		}
		System.out.println("Please enter sentence, when you're done press 'Tab' and then 'Enter':");
		String sentenceString = readTwistedSentence();

		java.util.ArrayList<String> sentenceArray = Untwist.stringToArrayList(sentenceString);

		System.out.println("Untwisting words as good as possible, please wait...");
		String untwistedSentence = Untwist.untwistSentence(sentenceArray);
		System.out.println("Untwisting done!\n");

		System.out.println(untwistedSentence);
	}

	private static String readTwistedSentence() {
		java.util.Scanner scan = new java.util.Scanner(System.in);
		String sentenceString = "";
		while (scan.hasNextLine()) {
			String input = scan.nextLine();
			sentenceString += input + "\n";
			if (input.contains("\t"))
				break;
		}
		scan.close();
		return sentenceString;
	}

	private static boolean checkForEnglish() {
		System.out.println("Also check for English words? (Less accuracy with german sentences).");
		System.out.print("[true/false] Answer: ");

		@SuppressWarnings("resource")
		java.util.Scanner scan = new java.util.Scanner(System.in);
		try {
			boolean alsoCheckForEnglish = scan.nextBoolean();
			return alsoCheckForEnglish;
		} catch (java.util.InputMismatchException ime) {
			System.out.println("\nInvalid answer, please try again!");
			System.out.println("------------------------------------");
			// restart method
			checkForEnglish();
		}
		// Unreachable code, because you have to return something
		return false;
	}
}