package aufgabe2;

import java.io.BufferedReader;
import java.io.FileInputStream;
import java.io.InputStreamReader;
import java.util.ArrayList;

/**
 * It'll untwist a sentence or a group of words, like "Hlelo Wrlod" to "Hello
 * World"
 * 
 */

public class Untwist {
  public static ArrayList<String> wordlist;

  /**
   * Woerter werden getrennt und in der Arraylist hinzugefuegt
   * 
   * @param sentence a group of words or a sentence
   * @return
   */
  public static ArrayList<String> stringToArrayList(String sentenceString) {

    ArrayList<String> sentenceList = new ArrayList<String>();
    String word = "";
    int i = 0;
    int lastLetter = sentenceString.length() - 1;

    while (i <= lastLetter) {
      boolean isLetter = Character.isLetter(sentenceString.charAt(i));

      while (Character.isLetter(sentenceString.charAt(i)) == isLetter && i <= lastLetter) {

        word += sentenceString.charAt(i);
        i++;
        if (i > lastLetter)
          break;
      }

      sentenceList.add(word);
      word = "";
    }
    return sentenceList;
  }

  /**
   * reads the wordlist .TXTs
   * 
   * @param alsoUseEnglish if it should also read the english wordlist.
   * @return ArrayList wordlist with a new String for Every Word.
   */
  public static ArrayList<String> readWordlist(boolean alsoUseEnglish) {
    // a new line equals a new word
    ArrayList<String> wordlist = new ArrayList<>();
    BufferedReader reader;
    String line;
    wordlist.addAll(readWordlist("german.txt"));
    wordlist.addAll(readWordlist("english.txt"));
    return wordlist;
  }

  /**
   * reads the wordlist .TXTs
   * 
   * @param wordlistPath Path of the wordlist file
   * @return ArrayList wordlist with a new String for Every Word.
   */
  public static ArrayList<String> readWordlist(String wordlistPath) {
    // a new line equals a new word
    ArrayList<String> wordlist = new ArrayList<>();
    BufferedReader reader;
    String line;
    try {
      // UTF-8 so it can handle german more accurate.
      reader = new BufferedReader(new InputStreamReader(new FileInputStream(wordlistPath), "UTF-8"));
      reader.readLine();
      while ((line = reader.readLine()) != null) {
        wordlist.add(line);
      }
      reader.close();
    } catch (java.io.IOException e) {
      System.err.println(e.toString());
    }
    return wordlist;
  }

  /**
   * untwisting the sentence
   * 
   * @param sentence Arraylist created before, from sentence
   * @return
   */
  public static String untwistSentence(ArrayList<String> sentence) {
    String output = "";
    for (String twistedWord : sentence) {
      output += untwistWord(twistedWord, wordlist) + "";
    }
    return output;
  }

  /**
   * untwisting the word
   * 
   * @param twistedWord twistedWord from untwistSentence
   * @param wordlist
   * @return
   */
  public static String untwistWord(String twistedWord, ArrayList<String> wordlist) {

    // first and last letter stay and the one in the middle has no to change
    // with
    boolean isWord = Character.isLetter(twistedWord.charAt(0));

    if (isWord) {

      boolean wordIsTwistable = twistedWord.length() > 3;

      if (wordIsTwistable) {

        String twistedWord_Lowered = 
            twistedWord.toLowerCase();
        char firstLetter_TwistedWord = 
            twistedWord_Lowered.charAt(0);
        char lastLetter_TwistedWord = 
            twistedWord_Lowered.charAt(twistedWord.length() - 1);
        String twistedWord_Sorted = SortString(twistedWord_Lowered);

        // go through each word in the list
        for (String currWord : wordlist) {

          boolean lengthOfWordsAreEqual = 
              currWord.length() == twistedWord.length();

          if (lengthOfWordsAreEqual) {

            String currWord_Lowered = currWord.toLowerCase();
            char firstLetter_CurrWord = currWord_Lowered.charAt(0);
            boolean firstLetters_AreEqual = 
                firstLetter_TwistedWord == firstLetter_CurrWord;

            if (firstLetters_AreEqual) {

              char lastLetter_CurrWord = 
                  currWord_Lowered.charAt(currWord.length() - 1);
              boolean lastLetters_AreEqual = 
                  lastLetter_CurrWord == lastLetter_TwistedWord;

              if (lastLetters_AreEqual) {
                // now it is likely to have the correct word

                // we now sort all letters alphabetical and
                // compare the letters with each other
                String currWord_Sorted = 
                    SortString(currWord_Lowered);
                boolean wordsAreEqual = 
                    currWord_Sorted.equals(twistedWord_Sorted);

                // not perfect but close enough
                if (wordsAreEqual) {
                  currWord = adaptCase(currWord, twistedWord);
                  return currWord;
                } // if
              } // if
            } // if
          } // if
        } // for
      } // if
    } // if

    // when the word can't be untwisted, it stays how it is
    return twistedWord;
  }

  /**
   * adapting the lower and upper case from twisted word to untwisted word
   * 
   * @param currWord    the untwisted word currently
   * @param twistedWord the twisted word from currWord
   * @return
   */
  private static String adaptCase(String currWord, String twistedWord) {
    StringBuilder wordBuilder = new StringBuilder(currWord);
    wordBuilder.setCharAt(0, twistedWord.charAt(0));
    currWord = wordBuilder.toString();
    return currWord;
  }

  /**
   * sorting the String
   * 
   * @param unsortedString the String which have to be sorted
   * @return
   */
  private static String SortString(String unsortedString) {
    char[] charArray = unsortedString.toCharArray();
    java.util.Arrays.sort(charArray);
    String sortedString = new String(charArray);
    return sortedString;
  }
}
