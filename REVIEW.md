# Review guide

- Lease Dispose가 인스턴스를 원래 pool에 정확히 한 번 반환하는지 확인합니다.
- Pool shutdown과 owner 파괴 순서가 바뀌어도 예외나 이중 반환이 없는지 확인합니다.
- 기반 GameObjectPooling 패키지에 Ownership 의존성을 역방향으로 추가하지 않습니다.
