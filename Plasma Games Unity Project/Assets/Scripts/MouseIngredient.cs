using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseIngredient : MonoBehaviour {
    [SerializeField]
    Sprite[] ingredientSprites;
    bool dropped = false;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    int ingredient = -1;
    FormulaHandler formulaHandler;
    void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        formulaHandler = FindObjectOfType<FormulaHandler>();
    }
    void Update() {
        if (!dropped && Input.GetMouseButtonDown(0) && ingredient != -1 && transform.position.x < 3.2f) {
            dropped = true;
        }
    }
    void FixedUpdate() {
        if (dropped) {
            transform.position = new Vector2(transform.position.x, transform.position.y-(3*Time.deltaTime));
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - (.5f*Time.deltaTime));
            return;
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        transform.position = mousePos;        
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (!dropped)
            return;

        if (other.name.Contains("Scene Bounds")) 
            return;      
            
        if (other.name.Contains("Flask"))
            formulaHandler.AddIngredient(ingredient);

        dropped = false; 
        sprite.enabled = false;
        ingredient = -1;
    }
    public void IngredientSelected(int ingredient) {
        if (dropped)
            return;

        sprite.color = Color.white;
        sprite.enabled = true;
        sprite.sprite = ingredientSprites[ingredient];
        this.ingredient = ingredient;
    }
}
