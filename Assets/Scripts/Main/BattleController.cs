using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState {
    START,
    PLAYERTURN,
    ENEMYTURN,
    WON,
    LOST
}

public class BattleController : MonoBehaviour
{
    public RangeFinder rangeFinder;
    public List<OverlayTile> rangeAttackTiles;
    public BattleState state; 

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    Character playerUnit;
    Character enemyUnit;

    public bool isAttack = false;
    public bool isDead;

    private void Start() {
        state = BattleState.START;
        rangeAttackTiles = new List<OverlayTile>();

        StartCoroutine("SetupBattle");
    }

    public void GetAttackRangeTiles(){
        isAttack = true;
        rangeAttackTiles = rangeFinder.GetTilesInRange(new Vector2Int(MapManager.Instance.character.standingOnTile.gridLocation.x,
        MapManager.Instance.character.standingOnTile.gridLocation.y),1);

        foreach(var item in rangeAttackTiles){
            item.SHowTile();
            //item.HIdeTile();
        }
        Invoke("AttackFalse",0.5f);
    }

    IEnumerator SetupBattle(){
        state = BattleState.PLAYERTURN;

        playerUnit = playerPrefab.GetComponent<Character>();
        enemyUnit = enemyPrefab.GetComponent<Character>();

        yield return new WaitForSeconds(2f);
        PlayerTurn();
    }

    void PlayerTurn(){
        Debug.Log("플레이어 턴");
    }

    public IEnumerator PlayerAttack(ScriptableCard _cardname){
        if (state != BattleState.PLAYERTURN) { yield return null; }

        GetAttackRangeTiles();

        yield return new WaitForSeconds(2f);


        switch(_cardname.CardType){
            case cardType.Skill1 : isDead = enemyUnit.TakeDamage(_cardname.attackDamage); break;
            default : break;
        }

        yield return new WaitForSeconds(2f);

        if(isDead){
            state = BattleState.WON;
            EndBattle();
        }
        else{
            state = BattleState.LOST;
        }
    }

    void EndBattle(){
        if(state == BattleState.WON){

        }
        else if(state == BattleState.LOST){

        }
    }

    IEnumerator EnemyTurn(ScriptableCard _cardname){
        GetAttackRangeTiles();

        switch (_cardname.CardType)
        {
            case cardType.Skill1: isDead = playerUnit.TakeDamage(_cardname.attackDamage); break;
            default: break;
        }

        yield return new WaitForSeconds(2f);

        if(isDead){
            state = BattleState.LOST;
            EndBattle();
        }
        else{
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    // void AttackFalse(){
    //     isAttack = false;
    // }
}
