using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable 
{
    void SetMaxHP(float hp, bool resetHealth);
    void SetCurrentHP(float hp);

    void TakeDmg(float dmg);
    void GetHealed(float heal);
    void Die();
}
