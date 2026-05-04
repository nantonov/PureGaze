terraform {
  backend "azurerm" {
    resource_group_name  = "pure_gaze_rg"
    storage_account_name = "puregaze2026notifsa"
    container_name       = "tfstate"
    key                  = "terraform.tfstate"
  }

  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 4.0"
    }
  }
}

provider "azurerm" {
  features {}
  subscription_id = var.azure_subscription_id
}
