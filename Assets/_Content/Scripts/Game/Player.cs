/**
 *  Простой персонаж, который все время бежит вперед, может перемещаться в стороны на максимальную ширину
 *  Эффекты выполнены в виде стратегии
 *  т.к. нет информации о том как эффекты взаимодействуют друг с другом и сколько эффектов разово может быть на персонаже
 *  сделал их в виде массива, где все эффекты должны прожить свой срок, а потом удалиться.
 *  такой подход может работать только при разно направленных эффектах.
 *  т.к. лететь и одновременно например плыть персонаж наверно не должен
 */

using System.Collections.Generic;
using _Content.Scripts.Game;
using _Content.Scripts.Game.PlayerEffects;
using _Content.Scripts.Services;
using _Content.Scripts.Settings;
using UnityEngine;
using VContainer;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform playerCenterTransform;
    
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _slideSpeed;

    public float RunSpeedMultiply { get; set; } = 1;
    public float SlideSpeedMultiply { get; set; } = 1;


    private List<AEffect> _effects = new ();
    private ControllerService _controller;
    private GameSettings _settings;

    [Inject]
    public void Construct(ControllerService controller, GameSettings settings)
    {
        _controller = controller;
        _settings = settings;
    }

    private void Update()
    {
        var dt = Time.deltaTime;
        MovePlayer(dt);
        RemoveOldEffect();
    }

    //Перемещение персонажа
    private void MovePlayer(float dt)
    {
        var horizontal = _controller.Horizontal;
        var limit = _settings.roadWide;

        var slide = Vector3.right * _slideSpeed * SlideSpeedMultiply * horizontal;
        var run = Vector3.forward * _runSpeed * RunSpeedMultiply;
        var moveTo = (slide + run) * dt;

        playerCenterTransform.Translate(moveTo);
        
        var position = playerCenterTransform.position;
        position.x = Mathf.Clamp(position.x, -limit, limit);
        playerCenterTransform.position = position;
    }

    //Наложение эффекта
    private void AddEffect(AEffect effect)
    {
        _effects.Add(effect);
        effect.Execute(this);
    }
    
    //Удаление эффекта с истекшим сроком
    private void RemoveOldEffect()
    {
        for (var i = 0; i < _effects.Count; i++)
        {
            if (_effects[i].IsRemoved)
                _effects.RemoveAt(i);
        }
    }

    //Коллизия персонажа с Collidable объектами
    private void OnTriggerEnter(Collider other)
    {
        var collidable = other.GetComponent<CollidableObject>();
        
        if(!collidable) return;
        
        //тк все объекты возвращаются в кэш просто скрываю объект
        other.gameObject.SetActive(false);
        AddEffect(collidable.GetEffect());
   }
}
