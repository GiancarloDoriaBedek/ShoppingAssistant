# ShoppingAssistant

## Table of Contents
* [General Info](#general-information)
* [Technologies Used](#technologies-used)
* [Features](#features)
* [Usage](#usage)
* [Room for Improvement](#room-for-improvement)
* [Acknowledgements](#acknowledgements)
<!-- * [License](#license) -->


## General Information
- This API is used to track and save price information about products from various webshops. This is achieved by connecting to this [scraper API](https://github.com/GiancarloDoriaBedek/webshop_scraper_api)

## Technologies Used
- C#
- .NET 6.0
- EF Core
- SQL SERVER

## Features
List the ready features here:
- Fully fledged authorization and authentication using JWT tokens
- Product search
- Price tracking through time

## Usage
- API endpoint used for gathering new data are hidden behind "Admin" role and are supposed to be run periodically using some kind of a scheduled action
- Gathered product data is split accros "Product", "Package" "Brand" and "Store" entities.
    - Product - Contains general information about product. Once created through initial scraping of that object, it is meant to persist. All subsequent data gatherings woll try to reference existing Product entity
    - Package - Contains information about pricing and packaging of the given Product. It is attached to the same existing product on every data gathering.
    - Brand - Contains general information about product brands
    - Store - Contains general information about the webstore

## Room for Improvement
- Enhance searching product by name capabilities
- Add searching and grouping products by brand
- Allow user to create list of item to track
