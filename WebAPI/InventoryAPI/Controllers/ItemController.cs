using InventoryAPI.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/items")]
[ApiController]

public class ItemController : ControllerBase
{
    //implement CRUD
    private static List<Item> items = new List<Item>();
    //Retrieve all Items
    [HttpGet]
    public ActionResult<List<Item>> GetAll() => items;

    //GET by Id: Retrieve Item by Id
    [HttpGet("{id}")]
    public ActionResult<Item> GetById(int id)
    {
        var item = items.FirstOrDefault(i => i.Id == id);
        return item != null ? Ok(item) : NotFound();
    }

    // POST: Add a new Item
    [HttpPost]
    public ActionResult<Item> Create(Item newItem)
    {
        newItem.Id = items.Count + 1;
        items.Add(newItem);
        return CreatedAtAction(nameof(GetById), new {id = newItem.Id}, newItem);
    }

    //PUT: Updates an existing Item
    [HttpPut("{id}")]
    public ActionResult Update(int id, Item updatedItem)
    {
        var item = items.FirstOrDefault(i=>i.Id == id);
        if (item == null) return NotFound();


        item.Name = updatedItem.Name;
        item.Description = updatedItem.Description;
        item.Price = updatedItem.Price;

        return Ok(item);
    }

    //REMOVE: Remove a item by Id
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var item = items.FirstOrDefault(i => i.Id==id);
        if (item == null) return NotFound();

        items.Remove(item);
        return NoContent();
    }
}