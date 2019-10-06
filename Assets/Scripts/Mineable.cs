using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineable : MonoBehaviour
{

	public MonoBehaviour script;

	public void Mine()
	{
		script.SendMessage("Mined");
	}
}
