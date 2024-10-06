using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpellingGame : MonoBehaviour
{
    public Image objectImage;              // Image to display the current object
    public TMP_InputField inputField;      // Input field where the player types the answer
    public TMP_Text feedbackText;          // Text to show "Correct" or "Wrong" message
    public AudioSource audioSource;        // Audio source to play object sounds
    public AudioClip correctSound;         // Sound to play when answer is correct
    public AudioClip wrongSound;           // Sound to play when answer is wrong

    public Sprite[] objectImages;          // Array of object images
    public string[] correctWords;          // Array of correct words for each image
    public AudioClip[] objectSounds;       // Array of sounds for each object

    private int currentQuestionIndex = 0;
    private int currentAudioIndex = -1;

    void Start()
    {
        LoadNextQuestion();
    }

    // This method is called when a letter button is clicked
    public void OnLetterButtonClicked(string letter)
    {
        inputField.text += letter;  // Append the clicked letter to the input field
    }

    // This method checks the input and provides feedback
    public void CheckAnswer()
    {
        string playerInput = inputField.text.ToLower();  // Convert input to lowercase
        string correctAnswer = correctWords[currentAudioIndex].ToLower();

        if (playerInput == correctAnswer)
        {
            feedbackText.text = "Correct!";
            feedbackText.color = Color.green;
            audioSource.PlayOneShot(correctSound);
            Invoke("LoadNextQuestion", 2f);  // Load the next question after 2 seconds
        }
        else
        {
            feedbackText.text = $"Wrong! Correct Answer: {correctAnswer}";
            feedbackText.color = Color.red;
            audioSource.PlayOneShot(wrongSound);
            Invoke("LoadNextQuestion", 4f);  // Show correct answer for 4 seconds, then load the next question
        }
    }

    public void PlayObjectAudioClip()
    {
        audioSource.PlayOneShot(objectSounds[currentAudioIndex]);  // Play the object's sound
    }

    // This method loads the next image, correct word, and sound
    void LoadNextQuestion()
    {
        if (currentQuestionIndex < objectImages.Length)
        {
            objectImage.sprite = objectImages[currentQuestionIndex];

            // Clear input field and feedback text
            inputField.text = "";
            feedbackText.text = "";

            currentQuestionIndex++;
            currentAudioIndex++;
        }
        else
        {
            feedbackText.text = "You've completed all questions!";
        }
    }
}

