# 🈯 Unilocalize

Localization tool for Unity that translates exclusively using a CSV file.

### Install from NPM
* Navigate to the `Packages` directory of your project.
* Adjust the [project manifest file](https://docs.unity3d.com/Manual/upm-manifestPrj.html) `manifest.json` in a text editor.
* Ensure `https://registry.npmjs.org/` is part of `scopedRegistries`.
  * Ensure `com.railek` is part of `scopes`.
  * Add `com.railek.unilocalize` to the `dependencies`, stating the latest version.

Please note that the version `X.Y.Z` as stated below is to be replaced with the latest version.

```json
{
  "scopedRegistries": [
    {
      "name": "npmjs",
      "url": "https://registry.npmjs.org/",
      "scopes": [
        "com.railek"
      ]
    }
  ],
  "dependencies": {
    "com.railek.unilocalize": "X.Y.Z",
    ...
  }
}
```

### License

This project is licensed under the MIT license, Copyright (c) 2020 Railek. For more information see [LICENSE](LICENSE).
