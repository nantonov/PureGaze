resource "azurerm_static_web_app" "static_app" {
  name                = "${var.project_prefix}-${var.environment}-swa"
  resource_group_name = azurerm_resource_group.rg.name
  location            = var.swa_location
  sku_tier            = var.swa_sku_tier
  sku_size            = var.swa_sku_size

  tags = {
    environment = var.environment
  }
}
