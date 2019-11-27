package twist;

import java.util.ArrayList;
import java.util.Random;
import java.util.Scanner;

public class Twist {
  /**
   * 
   */
  public static void twist() {
    Scanner scan = new Scanner(System.in);
    ArrayList<String> sentence = stringToArrayList(scan.nextLine());
    System.out.println("Twisting words, please wait");
    String output = twistSentence(sentence);
    System.out.println("Twisting done");
    System.out.println(output);
    scan.close();
  }

  /**
   * @param Normal sentence
   * @return Twisted sentence
   */
  private static String twistSentence(ArrayList<String> sentence) {
    String output = "";
    for (String untwistedWord : sentence) {
      output += twistWord(untwistedWord) + "";
    }
    return output;
  }

  /**
   * @param String with multiple words and -or character.
   * @return Words and characters saved separately in an ArrayList.
   */
  public static ArrayList<String> stringToArrayList(String sentence) {
    ArrayList<String> wordlist = new ArrayList<String>();
    String word = "";
    int i = 0;
    int lastLetter = sentence.length() - 1;
    while (i <= lastLetter) {
      if (Character.isLetter(sentence.charAt(i))) {
        while (Character.isLetter(sentence.charAt(i)) && i <= lastLetter) {
          word += sentence.charAt(i);
          i++;
          if (i > lastLetter)
            break;
        }
        wordlist.add(word);
        word = "";
      } else {
        while (!Character.isLetter(sentence.charAt(i)) && i <= lastLetter) {
          word += sentence.charAt(i);
          i++;
          if (i > lastLetter)
            break;
        }
        wordlist.add(word);
        word = "";
      }
    }
    return wordlist;
  }

  /**
   * @param word untwisted word
   * @return twisted word
   */
  private static String twistWord(String word) {
    if (word.length() > 3) {
      if (Character.isLetter(word.charAt(0))) {
        char[] fullUntwistedWord = word.toCharArray();
        char[] CharsToTwist = new char[fullUntwistedWord.length - 2];
        for (int i = 0; i < CharsToTwist.length; i++) {
          CharsToTwist[i] = fullUntwistedWord[i + 1];
        }
        char[] twistedChars = DurstenfeldShuffle(CharsToTwist);
        for (int i = 0; i < twistedChars.length; i++) {
          fullUntwistedWord[i + 1] = twistedChars[i];
        }
        return new String(fullUntwistedWord);
      }
    }
    return word;

  }

  /**
   * @param charArray
   * @return shuffled charArray
   */
  private static char[] DurstenfeldShuffle(char[] ar) {
    Random rnd = new Random();
    for (int i = ar.length - 1; i > 0; i--) {
      int index = rnd.nextInt(i + 1);
      // Simple swap
      char a = ar[index];
      ar[index] = ar[i];
      ar[i] = a;
    }
    return ar;
  }
}
