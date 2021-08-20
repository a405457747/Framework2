using UnityEngine;
using System.Collections;

// 遊戲子系統共用界面
public abstract class GameSystem: MonoBehaviour
{
	protected Game m_game = null;
	public GameSystem( Game game )
	{
		m_game = game;
	}

	public virtual void Initialize(){}
	public virtual void Release(){}
	public virtual void Update(){}

}
