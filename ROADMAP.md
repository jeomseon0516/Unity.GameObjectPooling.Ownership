# GameObject Pooling Ownership 로드맵

## 0.1

- GameObject 및 Component spawn 결과의 ownership lease
- `[ManagedPooledObject]` 생성 setter를 통한 교체 및 owner 파괴 자동 반환
- 생성된 lease ownership transfer 경로
- 런타임 구성 Pool Scene Sample

## 이후

- Pool shutdown과 owner destruction이 같은 프레임에 일어나는 순서 조합 확대 검증
- Borrowed instance escape 진단과 명시적 ownership transfer 패턴
