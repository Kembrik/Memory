using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
	public const int gridRows = 2;
	public const int gridCols = 4;
	public const float offsetX = 2f;
	public const float offsetY = 2.5f;

	[SerializeField] private MemoryCard originalCard;
	[SerializeField] private Sprite[] images;
	[SerializeField] private TextMesh scoreLabel;

	private MemoryCard _firstRevealed;
	private MemoryCard _secondRevealed;
	private int _score = 0;

	public bool canReveal
	{
		get { return _secondRevealed == null; }
	}

	void Start()
	{
		Vector3 StartPos = originalCard.transform.position;

		int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 };
		numbers = ShuffleArray(numbers);

		for (int x = 0; x < gridCols; x++)
		{
			for (int y = 0; y < gridRows; y++)
			{
				MemoryCard card;
				if (x == 0 && y == 0)
				{
					card = originalCard;
				}
				else
				{
					card = Instantiate(originalCard) as MemoryCard;
				}

				int index = y * gridCols + x;
				int id = numbers[index];
				// int id = Random.Range(0, images.Length);
				// originalCard.SetCard(id, images[id]);
				card.SetCard(id, images[id]);

				float posX = StartPos.x + (offsetX * x);
				float posY = StartPos.y - (offsetY * y);
				card.transform.position = new Vector3(posX, posY, StartPos.z);
			}
		}
	}

	private int[] ShuffleArray(int[] numbers)
	{
		int[] newArray = numbers.Clone() as int[];
		for (int i = 0; i < newArray.Length; i++)
		{
			int tmp = newArray[i];
			int r = Random.Range(0, newArray.Length);
			newArray[i] = newArray[r];
			newArray[r] = tmp;
		}
		return newArray;
	}

	public void CardRevealed(MemoryCard card)
	{
		if (_firstRevealed == null)
		{
			_firstRevealed = card;
		}
		else
		{
			_secondRevealed = card;
			StartCoroutine(CheckMatch());
		}
	}

	private IEnumerator CheckMatch()
	{
		if (_firstRevealed.id == _secondRevealed.id)
		{
			_score++;
			scoreLabel.text = "Score: " + _score;
		}
		else
		{
			yield return new WaitForSeconds(0.5f);
			_firstRevealed.Unrevealed();
			_secondRevealed.Unrevealed();
		}

		_firstRevealed = null;
		_secondRevealed = null;
	}

	public void Restart()
	{
		SceneManager.LoadScene("SampleScene");
		// Application.LoadLevel("Scene");
	}
}
