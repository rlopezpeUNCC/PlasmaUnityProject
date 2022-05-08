/*
    Faciliates the drag and droping of new ingredients.
*/
using UnityEngine.SceneManagement;
using UnityEngine;

public class MouseIngredient : MonoBehaviour {
    [SerializeField]
    Sprite[] ingredientSprites; // The sprites of all of the ingredients.
    bool dropped = false; // If an ingredient has been dropped.
    Rigidbody2D rb; // The rigid body of the ingredient.
    SpriteRenderer sprite; // The sprite component of the ingredient.
    int ingredient = -1; // The ID of the ingredent.
    FormulaHandler formulaHandler;
    AudioManager audioManager;
    void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        formulaHandler = FindObjectOfType<FormulaHandler>();
        audioManager = FindObjectOfType<AudioManager>();
    }
    void Update() {
        // Drops the ingredient if the mouse is clicked.
        if (!dropped && Input.GetMouseButtonUp(0) && ingredient != -1 && transform.position.x < 3.2f) {
            audioManager.Play("ItemEquiped1");
            dropped = true;
        }
    }
    void FixedUpdate() {
        // Moves the ingredient down and lowers the oppacity as it falls.
        if (dropped) {
            transform.position = new Vector2(transform.position.x, transform.position.y-(3*Time.deltaTime));
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - (.4f*Time.deltaTime));
            return;
        }
        // Moves the ingredient to follow the cursor
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        transform.position = mousePos;        
    }
    // Detects if the ingredent has collided with the flask or the out of bounds area.
    void OnTriggerEnter2D(Collider2D other) {
        // Returns if the ingredient has not been dropped.
        if (!dropped)
            return;
        if (other.name.Contains("Main Menu Flask")) {
            SceneManager.LoadScene("MainScene");
            return;
        }
        // Adds the ingredient to the list if it collides with the flask.
        if (other.name.Contains("Flask")) {
            formulaHandler.AddIngredient(ingredient);
            audioManager.Play("XPConsumed");
        }
        dropped = false; 
        sprite.enabled = false;
        ingredient = -1;
    }
    // Updates the selected ingredient.
    public void IngredientSelected(int ingredient) {
        if (dropped) {
            audioManager.Play("ShopFailed");
            return;
        }
        // Removes the opacity changes and enables the sprite.
        sprite.color = Color.white;
        sprite.enabled = true;
        // Updates the ingredient sprite to reflect the newly selected one.
        sprite.sprite = ingredientSprites[ingredient];
        this.ingredient = ingredient;
    }
}
